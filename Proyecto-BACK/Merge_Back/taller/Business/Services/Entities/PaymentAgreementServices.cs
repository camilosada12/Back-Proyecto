using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.IDataImplement.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Select.Entities;
using Entity.Infrastructure.Contexts;
using Entity.Init;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using Business.validaciones.Entities.PaymentAgreement;
using FluentValidation;

// 👇 alias para evitar ambigüedad
using FVValidationException = FluentValidation.ValidationException;
using UValidationException = Utilities.Exceptions.ValidationException;

namespace Business.Services.Entities
{
    public class PaymentAgreementServices
        : BusinessBasic<PaymentAgreementDto, PaymentAgreementSelectDto, PaymentAgreement>, IPaymentAgreementServices
    {
        private readonly ILogger<PaymentAgreementServices> _logger;
        private readonly IPaymentAgreementRepository _paymentAgreementRepository;
        private readonly ApplicationDbContext _context;

        public PaymentAgreementServices(
            IPaymentAgreementRepository paymentAgreementRepository,
            IMapper mapper,
            ILogger<PaymentAgreementServices> logger,
            ApplicationDbContext context
        ) : base(paymentAgreementRepository, mapper, context)
        {
            _paymentAgreementRepository = paymentAgreementRepository;
            _logger = logger;
            _context = context;
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

        public new async Task<PaymentAgreementSelectDto?> CreateAsync(PaymentAgreementDto dto)
        {
            try
            {
                BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");

                var createValidator = new PaymentAgreementDtoValidator<PaymentAgreementDto>();
                var validationResult = createValidator.Validate(dto);
                if (!validationResult.IsValid)
                    throw new FVValidationException(validationResult.Errors);

                var userInfraction = await _context.userInfraction
                    .Include(ui => ui.User)
                        .ThenInclude(u => u.Person)
                    .Include(ui => ui.typeInfraction)
                    .FirstOrDefaultAsync(ui => ui.id == dto.userInfractionId);

                if (userInfraction == null)
                    throw new BusinessException($"La infracción con ID {dto.userInfractionId} no existe.");

                if (userInfraction.stateInfraction != EstadoMulta.Pendiente)
                    throw new BusinessException(
                        $"La infracción con ID {dto.userInfractionId} no permite acuerdos de pago porque está en estado {userInfraction.stateInfraction}."
                    );

                var frequency = await _context.paymentFrequency.FindAsync(dto.paymentFrequencyId);
                if (frequency == null)
                    throw new BusinessException($"La frecuencia de pago con ID {dto.paymentFrequencyId} no existe.");

                var typePayment = await _context.typePayment.FindAsync(dto.typePaymentId);
                if (typePayment == null)
                    throw new BusinessException($"El método de pago con ID {dto.typePaymentId} no existe.");

                if (dto.Installments.HasValue && dto.MonthlyFee.HasValue)
                {
                    var total = dto.Installments.Value * dto.MonthlyFee.Value;

                    if (total != dto.BaseAmount)
                    {
                        throw new BusinessException(
                            $"El total de cuotas ({dto.Installments} x {dto.MonthlyFee} = {total}) no coincide con el monto base ({dto.BaseAmount})."
                        );
                    }
                }


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
                        ?? $"Acuerdo para {userInfraction.User.Person?.firstName} {userInfraction.User.Person?.lastName} - " +
                           $"Infracción: {userInfraction.typeInfraction.description}",
                    BaseAmount = dto.BaseAmount,
                    AccruedInterest = 0m,
                    OutstandingAmount = dto.BaseAmount,
                    IsPaid = false,
                    IsCoactive = false,
                    Installments = dto.Installments,
                    MonthlyFee = dto.MonthlyFee
                };

                userInfraction.stateInfraction = EstadoMulta.ConAcuerdoPago;
                _context.userInfraction.Update(userInfraction);

                var created = await _paymentAgreementRepository.CreateAsync(agreement);
                await _context.SaveChangesAsync();

                return _mapper.Map<PaymentAgreementSelectDto>(created);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al crear el acuerdo de pago.", ex);
            }
        }


        public override async Task<bool> UpdateAsync(PaymentAgreementDto dto)
        {
            try
            {
                BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");

                var updateValidator = new PaymentAgreementUpdateValidator();
                var validationResult = updateValidator.Validate(dto);
                if (!validationResult.IsValid)
                    throw new FVValidationException(validationResult.Errors); // 👈 alias usado

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
    }
}
