using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces.IBusinessImplements.Entities;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement.Entities;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services.Entities
{
    public class PaymentAgreementServices : BusinessBasic<PaymentAgreementDto,PaymentAgreementSelectDto,PaymentAgreement>,IPaymentAgreementServices
    {
        private readonly ILogger<PaymentAgreementServices> _logger;

        protected readonly IPaymentAgreementRepository _paymentAgreementRepository;

        public PaymentAgreementServices(IPaymentAgreementRepository paymentAgreementRepository, IMapper mapper, ILogger<PaymentAgreementServices> logger) : base(paymentAgreementRepository, mapper)
        {
            _paymentAgreementRepository = paymentAgreementRepository;
            _logger = logger;
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

                var entity = await _paymentAgreementRepository.GetByIdAsync(id);
                return _mapper.Map<PaymentAgreementSelectDto?>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }

        }
    }
}
