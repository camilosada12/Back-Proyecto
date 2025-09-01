using AutoMapper;
using Business.Interfaces.IBusinessImplements.Security;
using Business.Repository;
using Data.Interfaces.IDataImplement.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.LoginDto.response.RegisterReponseDto;
using Entity.DTOs.Default.ModelSecurityDto;
using Entity.DTOs.Default.RegisterRequestDto;
using Entity.DTOs.Select.ModelSecuritySelectDto;
using Entity.Infrastructure.Contexts;           // <-- DbContext
using Helpers.Business.Business.Helpers.Validation;
using Helpers.Initialize;
using Microsoft.EntityFrameworkCore;            // <-- Transacciones/consultas EF
using Microsoft.Extensions.Logging;
using Utilities.Custom;

namespace Business.Services.Security
{
    public class UserService : BusinessBasic<UserDto, UserSelectDto, User>, IUserService
    {
        private readonly ApplicationDbContext _db;      
        private readonly IPersonRepository _people;     
        private readonly IUserRepository _dataUser;
        private readonly ILogger<UserService> _logger;
        private readonly EncriptePassword _utilities;
        private readonly IRolUserService _rolUserService;

        public UserService(
            IUserRepository data,
            IPersonRepository people,                    
            ApplicationDbContext db,                     
            ILogger<UserService> logger,
            EncriptePassword utilities,
            IMapper mapper,
            IRolUserService rolUserService
        ) : base(data, mapper)
        {
            _dataUser = data;
            _people = people;                            
            _db = db;                                   
            _utilities = utilities;
            _logger = logger;
            _rolUserService = rolUserService;
        }

        // ========= Registro (Person + User + Rol por defecto) =========
        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            // 0) Validación de unicidad por email
            var existing = await _dataUser.FindEmail(dto.email);
            if (existing != null && !existing.is_deleted)
                throw new InvalidOperationException("Ya existe una cuenta con ese email.");

            await using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1) Crear Person
                var person = _mapper.Map<Person>(dto);
                person.InitializeLogicalState();
                var personCreated = await _people.CreateAsync(person);

                // 2) Crear User vinculado a la Person
                var user = _mapper.Map<User>(dto);
                user.PersonId = personCreated.id;

                user.InitializeLogicalState();
                var userCreated = await _dataUser.CreateAsync(user);

                // Persistir
                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                // 3) Asignar rol por defecto (no bloqueante)
                _ = _rolUserService.AsignateUserRTo(userCreated);

                return new RegisterResponseDto
                {
                    IsSuccess = true,
                    Message = "Registro completado."
                };
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error durante el registro de usuario.");
                throw;
            }
        }

        // ========= Crear usuario por Google (si no existe) =========
        public async Task<User> createUserGoogle(string email, string name)
        {
            var user = await _dataUser.FindEmail(email);
            if (user != null) return user;

            var newUser = new User
            {
                PasswordHash = null,
                email = email
            };

            await _dataUser.CreateAsync(newUser);
            return newUser;
        }

        // ========= Actualizar =========
        public async Task<bool> UpdateAsyncUser(UserDto dto)
        {
            BusinessValidationHelper.ThrowIfNull(dto, "El DTO no puede ser nulo.");
            var entity = _mapper.Map<User>(dto);

            // Si decides permitir cambio de password en el DTO:
            // if (!string.IsNullOrWhiteSpace(dto.PasswordPlain))
            //     entity.PasswordHash = _utilities.EncripteSHA256(dto.PasswordPlain);

            return await _dataUser.UpdateAsync(entity);
        }
    }
}
