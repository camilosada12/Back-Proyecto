using AutoMapper;
using Business.Interfaces.BusinessRegister;
using Business.Interfaces.IBusinessImplements.Security;
using Data.Interfaces.IDataImplement.Security;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.Auth.RegisterReponseDto;
using Entity.DTOs.Default.RegisterRequestDto;
using Entity.Infrastructure.Contexts;
using Helpers.Initialize;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Business.Services.Auth
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly ApplicationDbContext _db;
        private readonly IPersonRepository _people;
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;
        private readonly IRolUserService _roleUserService;

        public UserRegistrationService(
            ApplicationDbContext db,
            IPersonRepository people,
            IUserRepository users,
            IMapper mapper,
            IRolUserService roleUserService)
        {
            _db = db;
            _people = people;
            _users = users;
            _mapper = mapper;
            _roleUserService = roleUserService;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            // Unicidad por email en User (esto sigue siendo necesario)
            if (await _db.Set<User>().AnyAsync(u => u.email == dto.email && !u.is_deleted))
                throw new InvalidOperationException("Ya existe una cuenta con ese email.");

            using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                // 1. Crear Person
                var person = _mapper.Map<Person>(dto);
                person.InitializeLogicalState();
                var personCreated = await _people.CreateAsync(person);

                // 2. Crear User vinculado a esa Person
                var user = _mapper.Map<User>(dto);
                user.PersonId = personCreated.id;
                // user.password = _hasher.Hash(dto.password); // recomendado
                user.InitializeLogicalState();
                var userCreated = await _users.CreateAsync(user);

                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                // 3. Asignar rol por defecto (si aplica)
                _ = _roleUserService.AsignateUserRTo(userCreated);

                return new RegisterResponseDto
                {
                    IsSuccess = true,
                    PersonId = personCreated.id,
                    UserId = userCreated.id,
                    Message = "Registro completado."
                };
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
