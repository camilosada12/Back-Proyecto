using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Interfaces.PDF;
using Business.Mensajeria.Email.implements;
using Business.Mensajeria.Email.@interface;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Business.validaciones.Entities.PaymentAgreement;
using Data.Interfaces.IDataImplement.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Select.Entities;
using Entity.Infrastructure.Contexts;
using Entity.Init;
using FluentValidation;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
// 👇 alias para evitar ambigüedad
using FVValidationException = FluentValidation.ValidationException;

namespace Business.Services.Entities
{
    public class PaymentAgreementServices
        : BusinessBasic<PaymentAgreementDto, PaymentAgreementSelectDto, PaymentAgreement>, IPaymentAgreementServices
    {
        private readonly ILogger<PaymentAgreementServices> _logger;
        private readonly IPaymentAgreementRepository _paymentAgreementRepository;
        private readonly ApplicationDbContext _context;
        private readonly EmailBackgroundQueue _emailQueue;
        private readonly IServiceScopeFactory _scopeFactory;

        public PaymentAgreementServices(
            IPaymentAgreementRepository paymentAgreementRepository,
            IMapper mapper,
            ILogger<PaymentAgreementServices> logger,
            ApplicationDbContext context,
            EmailBackgroundQueue emailQueue,
            IServiceScopeFactory scopeFactory
        ) : base(paymentAgreementRepository, mapper, context)
        {
            _paymentAgreementRepository = paymentAgreementRepository;
            _logger = logger;
            _context = context;
            _emailQueue = emailQueue;
            _scopeFactory = scopeFactory;
        }


        public override async Task<IEnumerable<PaymentAgreementSelectDto>> GetAllAsync(GetAllType getAllType)
        {
            try
            {
                var strategy = GetStrategyFactory.GetStrategyGet(_paymentAgreementRepository, getAllType);
                var entities = await strategy.GetAll(_paymentAgreementRepository);
                return _mapper.Map<IEnumerable<PaymentAgreementSelectDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al obtener todos los registros.", ex);
            }
        }

        public override async Task<PaymentAgreementSelectDto?> GetByIdAsync(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                if (!await ExistsAsync(id))
                    throw new BusinessException($"El acuerdo de pago con ID {id} no existe.");

                var entity = await _paymentAgreementRepository.GetByIdAsync(id);
                return _mapper.Map<PaymentAgreementSelectDto?>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }
        }

        public new async Task<PaymentAgreementSelectDto> CreateAsync(PaymentAgreementDto dto)
        {
            // 1️⃣ Crear el acuerdo de pago en la base (internamente maneja validaciones, montos, estado de infracción, etc.)
            var entity = await CreatePaymentAgreementInternalAsync(dto);

            // 2️⃣ Mapear la entidad a DTO seguro
            var resultDto = _mapper.Map<PaymentAgreementSelectDto>(entity);

            // 3️⃣ Enviar correo en background usando la cola
            await _emailQueue.QueueBackgroundWorkItemAsync(async () =>
            {
                try
                {
                    // Crear un scope para servicios Scoped como DbContext o repositorios
                    using var scope = _scopeFactory.CreateScope();

                    var emailService = scope.ServiceProvider.GetRequiredService<IServiceEmail>();
                    var pdfService = scope.ServiceProvider.GetRequiredService<IPdfGeneratorService>();
                    var userInfractionRepo = scope.ServiceProvider.GetRequiredService<IUserInfractionRepository>();

                    // 3a️⃣ Traer la infracción completa asociada al acuerdo
                    var infraction = await userInfractionRepo.GetByIdAsync(entity.userInfractionId);
                    if (infraction == null || infraction.User == null || string.IsNullOrWhiteSpace(infraction.User.email))
                        return; // Si no hay email válido, no hacemos nada

                    // 3b️⃣ Mapear DTO seguro para generar PDF
                    var dtoForPdf = _mapper.Map<PaymentAgreementSelectDto>(entity);

                    // 3c️⃣ Generar PDF del acuerdo
                    var pdfBytes = await pdfService.GeneratePaymentAgreementPdfAsync(dtoForPdf);

                    // 3d️⃣ Construir email con builder específico
                    var builder = new PaymentAgreementEmailBuilder(dtoForPdf, pdfBytes);

                    // 3e️⃣ Enviar correo
                    await emailService.SendEmailAsync(
                        infraction.User.email,
                        builder.GetSubject(),
                        builder.GetBody(),
                        builder.GetAttachments()
                    );

                    // 3f️⃣ Log de éxito
                    _logger.LogInformation(
                        "Correo enviado correctamente a {Email} para el acuerdo de pago {Id}",
                        infraction.User.email,
                        entity.id
                    );
                }
                catch (Exception ex)
                {
                    // Log de error en el envío, pero no afecta la creación del acuerdo
                    _logger.LogError(ex, "Error enviando correo para el acuerdo de pago {Id}", resultDto.Id);
                }
            });

            // 4️⃣ Retornar DTO seguro
            return resultDto;
        }



        // Método interno para la creación de PaymentAgreement
        private async Task<PaymentAgreement> CreatePaymentAgreementInternalAsync(PaymentAgreementDto dto)
        {
            BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");

            // Validación DTO
            var createValidator = new PaymentAgreementDtoValidator<PaymentAgreementDto>();
            var validationResult = createValidator.Validate(dto);
            if (!validationResult.IsValid)
                throw new FVValidationException(validationResult.Errors);

            // Traer la infracción con detalles
            var userInfraction = await _paymentAgreementRepository.GetUserInfractionWithDetailsAsync(dto.userInfractionId);
            if (userInfraction == null)
                throw new BusinessException($"La infracción con ID {dto.userInfractionId} no existe.");

            if (userInfraction.stateInfraction != EstadoMulta.Pendiente)
                throw new BusinessException(
                    $"La infracción con ID {dto.userInfractionId} no permite acuerdos de pago porque está en estado {userInfraction.stateInfraction}."
                );

            // Validar frecuencia y tipo de pago
            var frequency = await _paymentAgreementRepository.GetPaymentFrequencyAsync(dto.paymentFrequencyId)
                ?? throw new BusinessException($"La frecuencia de pago con ID {dto.paymentFrequencyId} no existe.");

            var typePayment = await _paymentAgreementRepository.GetTypePaymentAsync(dto.typePaymentId)
                ?? throw new BusinessException($"El método de pago con ID {dto.typePaymentId} no existe.");

            // Calcular montos
            var (baseAmount, installments, monthlyFee) = CalcularMontos(userInfraction, dto);

            // Crear entidad
            var agreement = new PaymentAgreement
            {
                AgreementStart = dto.AgreementStart,
                AgreementEnd = dto.AgreementEnd,
                expeditionCedula = dto.expeditionCedula,
                userInfractionId = dto.userInfractionId,
                paymentFrequencyId = dto.paymentFrequencyId,
                typePaymentId = dto.typePaymentId,
                address = dto.address,
                neighborhood = dto.neighborhood,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                AgreementDescription = dto.AgreementDescription
                    ?? $"Acuerdo para {userInfraction.User.Person?.firstName} {userInfraction.User.Person?.lastName} - Infracción: {userInfraction.typeInfraction.description}",
                BaseAmount = baseAmount,
                AccruedInterest = 0m,
                OutstandingAmount = baseAmount,
                IsPaid = false,
                IsCoactive = false,
                Installments = installments,
                MonthlyFee = monthlyFee
            };

            // Cambiar estado de la infracción
            userInfraction.stateInfraction = EstadoMulta.ConAcuerdoPago;
            _context.userInfraction.Update(userInfraction);

            var created = await _paymentAgreementRepository.CreateAsync(agreement);
            await _context.SaveChangesAsync();

            return created;
        }


        public override async Task<bool> UpdateAsync(PaymentAgreementDto dto)
        {
            try
            {
                BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");

                var updateValidator = new PaymentAgreementUpdateValidator();
                var validationResult = updateValidator.Validate(dto);
                if (!validationResult.IsValid)
                    throw new FVValidationException(validationResult.Errors);

                var entity = await _context.paymentAgreement.FindAsync(dto.id);
                if (entity == null)
                    throw new BusinessException($"El acuerdo de pago con ID {dto.id} no existe.");

                entity.Installments = dto.Installments;
                entity.MonthlyFee = dto.MonthlyFee;

                _context.paymentAgreement.Update(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al actualizar el acuerdo de pago.", ex);
            }
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                if (!await ExistsAsync(id))
                    throw new BusinessException($"No se puede eliminar. El acuerdo de pago con ID {id} no existe.");

                return await base.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al eliminar el registro con ID {id}.", ex);
            }
        }

        public override async Task<bool> RestoreLogical(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                if (!await ExistsAsync(id))
                    throw new BusinessException($"No se puede restaurar. El acuerdo de pago con ID {id} no existe.");

                return await base.RestoreLogical(id);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al restaurar el registro con ID {id}.", ex);
            }
        }

        public async Task<int> ApplyLateFeesAsync(DateTime nowUtc, CancellationToken ct = default)
        {
            int updated = 0;
            DateTime today = nowUtc.Date;

            var agreements = await _context.paymentAgreement
                .Where(a => !a.is_deleted && !a.IsPaid)
                .ToListAsync(ct);

            foreach (var a in agreements)
            {
                DateTime coactiveDate = a.AgreementStart.Date.AddDays(30);

                if (today >= coactiveDate && !a.IsCoactive)
                {
                    a.IsCoactive = true;
                    a.CoactiveActivatedOn = coactiveDate;
                    a.LastInterestAppliedOn = coactiveDate.AddDays(-1);
                }

                if (a.IsCoactive)
                {
                    DateTime lastApplied = a.LastInterestAppliedOn?.Date
                        ?? a.CoactiveActivatedOn!.Value.AddDays(-1);

                    int daysToAccrue = (today - lastApplied).Days;

                    if (daysToAccrue > 0)
                    {
                        decimal monthlyRate = 0.02m;
                        int divisor = 30;
                        decimal dailyRate = monthlyRate / divisor;

                        decimal interestToAdd = a.OutstandingAmount * dailyRate * daysToAccrue;

                        a.AccruedInterest += interestToAdd;
                        a.OutstandingAmount = a.BaseAmount + a.AccruedInterest;
                        a.LastInterestAppliedOn = today;

                        updated++;
                    }
                }
            }

            if (updated > 0)
                await _context.SaveChangesAsync(ct);

            return updated;
        }

        public async Task<IEnumerable<PaymentAgreementInitDto>> GetInitDataAsync(int userInfractionId)
        {
            return await _paymentAgreementRepository.GetInitDataAsync(userInfractionId);
        }

        public (decimal BaseAmount, int Installments, decimal MonthlyFee) CalcularMontos(
            UserInfraction userInfraction,
            PaymentAgreementDto dto)
        {
            var detail = userInfraction.typeInfraction.fineCalculationDetail
                .OrderByDescending(fd => fd.valueSmldv.Current_Year)
                .FirstOrDefault();

            if (detail == null)
                throw new BusinessException("No existe detalle de cálculo para esta infracción.");

            // ✅ Siempre recalculamos el monto base en runtime
            decimal baseAmount = userInfraction.typeInfraction.numer_smldv * (decimal)detail.valueSmldv.value_smldv;


            // Número de cuotas (por defecto 1 si no viene en el DTO)
            int installments = dto.Installments ?? 1;

            // Cuota mensual redondeada
            decimal monthlyFee = Math.Round(
                baseAmount / installments,
                0,
                MidpointRounding.AwayFromZero
            );

            // Validación si el frontend envía cuotas + valor
            if (dto.Installments.HasValue && dto.MonthlyFee.HasValue)
            {
                var total = dto.Installments.Value * dto.MonthlyFee.Value;
                if (total != baseAmount)
                    throw new BusinessException(
                        $"El total de cuotas ({dto.Installments} x {dto.MonthlyFee} = {total}) no coincide con el monto base ({baseAmount})."
                    );
            }

            return (baseAmount, installments, monthlyFee);
        }

        public Task<PaymentAgreementSelectDto> GetByIdAsyncPdf(int id)
        {
            throw new NotImplementedException();
        }
    }
}
