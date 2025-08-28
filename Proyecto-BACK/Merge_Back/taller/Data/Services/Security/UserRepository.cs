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



        public async Task<User?> FindByEmailAsync(string emailNorm)
        {
            if (string.IsNullOrWhiteSpace(emailNorm)) return null;
            var norm = emailNorm.Trim().ToLowerInvariant();

            // Si quieres incluir Person para reglas posteriores:
            // return await _dbSet.Include(u => u.Person)
            //     .FirstOrDefaultAsync(u => u.email != null && u.email.ToLower() == norm);

            return await _dbSet
                .FirstOrDefaultAsync(u => u.email != null && u.email.ToLower() == norm);
        }

        public async Task<User?> FindByDocumentAsync(int documentTypeId, string documentNumber)
        {
            if (documentTypeId <= 0 || string.IsNullOrWhiteSpace(documentNumber)) return null;
            var docNum = documentNumber.Trim();

            // Si quieres incluir Person:
            // return await _dbSet.Include(u => u.Person)
            //     .FirstOrDefaultAsync(u => u.documentTypeId == documentTypeId && u.documentNumber == docNum);

            return await _dbSet
                .FirstOrDefaultAsync(u => u.documentTypeId == documentTypeId && u.documentNumber == docNum);
        }

        public Task<bool> VerifyPasswordAsync(User user, string plainPassword)
        {
            if (user == null || string.IsNullOrEmpty(plainPassword)) return Task.FromResult(false);
            if (string.IsNullOrEmpty(user.PasswordHash)) return Task.FromResult(false);

            // 1) PBKDF2 (formato: pbkdf2-sha256$iter$salt$hash)
            if (user.PasswordHash.StartsWith("pbkdf2-sha256$", StringComparison.Ordinal))
                return Task.FromResult(_encriptePass.Verify(plainPassword, user.PasswordHash));

            // 2) Fallback temporal: seeds en claro
            if (string.Equals(user.PasswordHash, plainPassword, StringComparison.Ordinal))
                return Task.FromResult(true);

            // (Si tuvieras otros formatos legados, agrégalos aquí)

            return Task.FromResult(false);
        }

        public async Task<User?> FindEmail(string email)
        {
            var user = await _dbSet.Where(u => u.email == email).FirstOrDefaultAsync();
            return user;
        }
        //public async Task<User> ValidateUserAsync(LoginDto loginDto)
        //{
        //    // Detecta modo de forma estricta (evita basura de Swagger)
        //    bool usesEmail = !string.IsNullOrWhiteSpace(loginDto.Email) && !string.IsNullOrWhiteSpace(loginDto.Password);
        //    bool usesDoc = loginDto.DocumentTypeId.HasValue && loginDto.DocumentTypeId.Value > 0
        //                     && !string.IsNullOrWhiteSpace(loginDto.DocumentNumber);

        //    if (usesEmail == usesDoc)
        //        throw new ValidationException("Proporcione exactamente un modo de autenticación: (Email+Password) o (DocumentTypeId+DocumentNumber).");

        //    User? user = null;

        //    if (usesEmail)
        //    {
        //        // Normaliza
        //        var email = loginDto.Email!.Trim().ToLowerInvariant();
        //        var pass = loginDto.Password!.Trim();

        //        user = await _dbSet
        //            .FirstOrDefaultAsync(u => u.email != null && u.email.ToLower() == email);

        //        if (user is null)
        //            throw new UnauthorizedAccessException("Credenciales inválidas.");

        //        // ⚠️ Mientras tus seeds están en texto plano, compara directo.
        //        // Cuando migres a hash, reemplaza por tu verificador (BCrypt/PBKDF2/etc.)
        //        if (!string.Equals(user.PasswordHash, pass, StringComparison.Ordinal))
        //            throw new UnauthorizedAccessException("Credenciales inválidas.");
        //    }
        //    else // usesDoc
        //    {
        //        int docType = loginDto.DocumentTypeId!.Value;
        //        string docNum = loginDto.DocumentNumber!.Trim();

        //        user = await _dbSet
        //            .FirstOrDefaultAsync(u => u.documentTypeId == docType && u.documentNumber == docNum);

        //        if (user is null)
        //            throw new UnauthorizedAccessException("Credenciales inválidas.");
        //        // Nota: en modo documento NO se valida contraseña
        //    }

        //    return user;
        //}

    }
}
