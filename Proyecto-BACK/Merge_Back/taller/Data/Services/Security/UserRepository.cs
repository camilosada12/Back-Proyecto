using Data.Interfaces.IDataImplement.Security;
using Data.Repositoy;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.DTOs.Default.LoginDto;
using Entity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Utilities.Custom;

namespace Data.Services.Security
{
    public class UserRepository: DataGeneric<User>, IUserRepository
    {
        private readonly EncriptePassword _encriptePass;

        public UserRepository(ApplicationDbContext context, EncriptePassword encriptePass) : base(context)
        {
            _encriptePass = encriptePass;
        }



        public async Task<User?> FindEmail(string email)
        {
            var user = await _dbSet.Where(u => u.email == email).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> ValidateUserAsync(LoginDto loginDto)
        {
            // Detecta modo de forma estricta (evita basura de Swagger)
            bool usesEmail = !string.IsNullOrWhiteSpace(loginDto.Email) && !string.IsNullOrWhiteSpace(loginDto.Password);
            bool usesDoc = loginDto.DocumentTypeId.HasValue && loginDto.DocumentTypeId.Value > 0
                             && !string.IsNullOrWhiteSpace(loginDto.DocumentNumber);

            if (usesEmail == usesDoc)
                throw new ValidationException("Proporcione exactamente un modo de autenticación: (Email+Password) o (DocumentTypeId+DocumentNumber).");

            User? user = null;

            if (usesEmail)
            {
                // Normaliza
                var email = loginDto.Email!.Trim().ToLowerInvariant();
                var pass = loginDto.Password!.Trim();

                user = await _dbSet
                    .FirstOrDefaultAsync(u => u.email != null && u.email.ToLower() == email);

                if (user is null)
                    throw new UnauthorizedAccessException("Credenciales inválidas.");

                // ⚠️ Mientras tus seeds están en texto plano, compara directo.
                // Cuando migres a hash, reemplaza por tu verificador (BCrypt/PBKDF2/etc.)
                if (!string.Equals(user.PasswordHash, pass, StringComparison.Ordinal))
                    throw new UnauthorizedAccessException("Credenciales inválidas.");
            }
            else // usesDoc
            {
                int docType = loginDto.DocumentTypeId!.Value;
                string docNum = loginDto.DocumentNumber!.Trim();

                user = await _dbSet
                    .FirstOrDefaultAsync(u => u.documentTypeId == docType && u.documentNumber == docNum);

                if (user is null)
                    throw new UnauthorizedAccessException("Credenciales inválidas.");
                // Nota: en modo documento NO se valida contraseña
            }

            return user;
        }

    }
}
