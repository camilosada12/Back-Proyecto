using AutoMapper;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Repository;
using Business.Strategy.StrategyGet.Implement;
using Data.Interfaces.DataBasic;
using Data.Interfaces.IDataImplement.Security;
using Data.Services;
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services.Security
{
    public class RolFormPermissionService : BusinessBasic<RolFormPermissionDto, RolFormPermissionSelectDto, RolFormPermission>, IRolFormPermissionService
    {

        private readonly IRolFormPermissionRepository _rolFormPermissionRepository;
        private readonly ILogger _logger;
        public RolFormPermissionService(IData<RolFormPermission> data, IMapper mapper, IRolFormPermissionRepository rolFormPermissionRepository, ILogger<RolFormPermissionService> logger) : base(data, mapper)
        {
            _rolFormPermissionRepository = rolFormPermissionRepository;
            _logger = logger;

        }

        public override async Task<IEnumerable<RolFormPermissionSelectDto>> GetAllAsync(GetAllType getAllType)
        {
            try
            {
                //var entity = await _formModuleRepository.GetAllAsync();
                //return _mapper.Map<IEnumerable<FormModuleSelectDto>>(entity);

                var strategy = GetStrategyFactory.GetStrategyGet(_rolFormPermissionRepository, getAllType);
                var entities = await strategy.GetAll(_rolFormPermissionRepository);
                return _mapper.Map<IEnumerable<RolFormPermissionSelectDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al obtener todos los registros.", ex);
            }
        }
        public override async Task<RolFormPermissionSelectDto?> GetByIdAsync(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                var entity = await _rolFormPermissionRepository.GetByIdAsync(id);
                return _mapper.Map<RolFormPermissionSelectDto?>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }

        }

        
    }
}
