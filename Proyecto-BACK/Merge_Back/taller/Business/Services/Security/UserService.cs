using AutoMapper;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Repository;
using Data.Interfaces.IDataImplement.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Helpers.Business.Business.Helpers.Validation;
using Helpers.Initialize;
using Microsoft.Extensions.Logging;
using Utilities.Custom;

namespace Business.Services.Security
{
    public class UserService : BusinessBasic<UserDto, UserSelectDto, User>, IUserService
    {
        private readonly IUserRepository _dataUser;
        private readonly ILogger<UserService> _logger;
        private readonly EncriptePassword _utilities;

        private readonly IRolUserService _rolUserService;

        //protected override IData<User> Data => _unitOfWork.Users;

        public UserService(IUserRepository data, ILogger<UserService> logger, EncriptePassword utilities, IMapper mapper, IRolUserService rolUserService) : base(data, mapper)
        {
            _dataUser = data;
            _utilities = utilities;
            _logger = logger;
            _rolUserService = rolUserService;
        }

        //public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper): base(unitOfWork, mapper) 
        //{
        //    _logger = logger;
        //}

        //protected override void ValidateDto(UserDto dto)
        //{
        //    if (dto == null)
        //    {
        //        throw new ValidationException("El objeto Rol no puede ser nulo");
        //    }

        //}

        //protected async override Task ValidateIdAsync(int id)
        //{
        //    var entity = await _dataUser.GetByIdAsync(id);
        //    if (entity == null)
        //    {
        //        _logger.LogWarning($"Se intentó operar con un ID inválido: {id}");
        //        throw new EntityNotFoundException($"No se encontró una Rol con el ID {id}");
        //    }

        //}

        //Nuevo metodo 

        public async Task<UserDto> CreateAsyncUser(UserDto dto)
        {
            BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");

            // Mapeamos primero
            var userEntity = _mapper.Map<User>(dto);


            if (userEntity.PersonId <= 0)
                throw new ArgumentException("PersonId es obligatorio y debe ser mayor que cero.");
            // Luego encripto la contraseña antes de guardar
            //userEntity.password = _utilities.EncripteSHA256(userEntity.password);
            //userEntity.password = userEntity.password;

            userEntity.InitializeLogicalState(); // Inicializa estado lógico (is_deleted = false)
            var createdEntity = await _dataUser.CreateAsync(userEntity);

            _ = _rolUserService.AsignateUserRTo(createdEntity);

            return _mapper.Map<UserDto>(createdEntity);
        }

        // Crear
        public async Task<User> createUserGoogle(string email, string name)
        {
            var user = await _dataUser.FindEmail(email);

            if (user != null) return user;

            var newUser = new User
            {
                name = name,
                //password = _utilities.EncripteSHA256("hola"),
                password = null,
                email = email
            };


            await _dataUser.CreateAsync(newUser);
            return newUser;
        }

        //Actalizar
        public async Task<bool> UpdateAsyncUser(UserDto dto)
        {
            BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");
            var entity = _mapper.Map<User>(dto);
            //entity.password = _utilities.EncripteSHA256(entity.password);
            entity.password = entity.password;

            return await _dataUser.UpdateAsync(entity);
        }


        
    }
}
