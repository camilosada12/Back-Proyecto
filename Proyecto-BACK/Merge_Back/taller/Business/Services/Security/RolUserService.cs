﻿using AutoMapper;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Repository;
using Data.Interfaces.IDataImplement.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Helpers.Initialize;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business.Services.Security
{
    public class RolUserService : BusinessBasic<RolUserDto, RolUserSelectDto, RolUser>, IRolUserService
    {

        private readonly IRolUserRepository _dataRolUser;
        //private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<RolUserService> _logger;
        //protected readonly IData<RolUser> Data;

        public RolUserService(IRolUserRepository data, ILogger<RolUserService> logger, IMapper mapper) : base(data, mapper)
        {
            _dataRolUser = data;
            _logger = logger;

        }

        public async Task<RolUserDto> AsignateUserRTo(User user)
        {
            try
            {
                var entity = await _dataRolUser.AsignateUserRTo(user);
                entity.InitializeLogicalState(); // Inicializa estado lógico (is_deleted = false)

                return _mapper.Map<RolUserDto>(entity); // No IEnumerable
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al asignar el rol al usuario.", ex);
            }
        }


        public override async Task<IEnumerable<RolUserSelectDto>> GetAllAsync()
        {
            try
            {
                var entity = await _dataRolUser.GetAllAsync();
                return _mapper.Map<IEnumerable<RolUserSelectDto>>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException("Error al obtener todos los registros.", ex);
            }
           
        }

        public  async Task<IEnumerable<string>> GetAllRolUser(int idUser)
        {

            var entity = await _dataRolUser.GetJoinRolesAsync(idUser);
            return entity;
        }

        public async Task<RolUserSelectDto?> GetByIdJoin(int id)
        {
            try
            {
                BusinessValidationHelper.ThrowIfZeroOrLess(id, "El ID debe ser mayor que cero.");

                var entity = await _dataRolUser.GetByIdAsync(id);
                return entity == null ? default : _mapper.Map<RolUserSelectDto>(entity);
            }
            catch (Exception ex)
            {
                throw new BusinessException($"Error al obtener el registro con ID {id}.", ex);
            }
            
        }

        



        //public async Task<RolUserDto> CreateAsync(RolUserDto dto)
        //{
        //    ValidateDto(dto);
        //    var entity = _mapper.Map<RolUser>(dto);
        //    var created = await Data.CreateAsync(entity);
        //    return _mapper.Map<RolUserDto>(created);
        //}

        //protected override void ValidateDto(RolUserDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ValidationException("El objeto RolUser no puede ser nulo");
        //    }
        //}

        //protected override async Task ValidateIdAsync(int id)
        //{
        //    var entity = await _dataRolUser.GetByIdAsync(id);
        //    if (entity == null) 
        //    {
        //        _logger.LogWarning($"Se intentó operar con un ID inválido: {id}");
        //        throw new EntityNotFoundException($"No se encontró una RolUser con el ID {id}");
        //    }
        //}

        //protected override void ValidateDto(RolUserDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ValidationException("El objeto RolUser no puede ser nulo");
        //    }
        //}
    }
}
