
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Interfaces.IBusinessImplements.parameters;
using Business.Repository;
using Business.Services.Entities;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement.parameters;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.Entities;
using Entity.Domain.Models.Implements.parameters;
using Entity.DTOs.Default.parameters;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services.parameters
{
    public class municipalityServices : BusinessBasic<municipalityDto,municipalitySelectDto,municipality>,ImunicipalityServices
    {
        private readonly ILogger<municipalityServices> _logger;

        protected readonly ImunicipalityRepository _municipalityRepository;

        public municipalityServices(ImunicipalityRepository MunicipalityRepository, IMapper mapper, ILogger<municipalityServices> logger) : base(MunicipalityRepository, mapper)
        {
            _municipalityRepository = MunicipalityRepository;
            _logger = logger;
        }

        public override async Task<IEnumerable<municipalitySelectDto>> GetAllAsync(GetAllType getAllType)
        {
            try
            {
                //var entity = await _formModuleRepository.GetAllAsync();
                //return _mapper.Map<IEnumerable<FormModuleSelectDto>>(entity);

                var strategy = GetStrategyFactory.GetStrategyGet(_municipalityRepository, getAllType);
                var entities = await strategy.GetAll(_municipalityRepository);
                return _mapper.Map<IEnumerable<municipalitySelectDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al obtener todos los registros.", ex);
            }
        }

        public override async Task<municipalitySelectDto?> GetByIdAsync(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                var entity = await _municipalityRepository.GetByIdAsync(id);
                return _mapper.Map<municipalitySelectDto?>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }

        }
    }
}
