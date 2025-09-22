using Business.Mensajeria.Email.@interface;
using Data.Services;
using Entity.Domain.Enums; // Aquí defines UserStatus
using Entity.Infrastructure.Contexts;
using Helpers.CodigoVerification;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Mensajeria.Email.implements
{
    public class VerificationService : IVerificationService
    {
        private readonly EmailBackgroundQueue _emailQueue;
        private readonly IServiceProvider _scopeFactory;
        private readonly VerificationCache _cache;

        public VerificationService(
            EmailBackgroundQueue emailQueue,
            IServiceProvider scopeFactory,
            VerificationCache cache)
        {
            _emailQueue = emailQueue;
            _scopeFactory = scopeFactory;
            _cache = cache;
        }

        // Registro inicial
        public async Task SendVerificationAsync(string nombre, string email)
        {
            var code = CodeGenerator.GenerateNumericCode();

            // Guardar en caché con duración de 24h
            _cache.SaveCode(email, code, TimeSpan.FromHours(24));

            // Actualizar en BD
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = db.users.FirstOrDefault(u => u.email == email);

            if (user != null)
            {
                user.Status = UserStatus.Pending;
                user.EmailVerificationCode = code;
                user.EmailVerificationExpiresAt = DateTime.UtcNow.AddHours(24);
                await db.SaveChangesAsync();
            }

            // Envío en segundo plano
            await _emailQueue.QueueBackgroundWorkItemAsync(async () =>
            {
                var builder = new VerificacionEmailBuilder(nombre, code);
                using var innerScope = _scopeFactory.CreateScope();
                var emailSender = innerScope.ServiceProvider.GetRequiredService<IServiceEmail>();
                await emailSender.SendEmailAsync(email, builder.GetSubject(), builder.GetBody());
            });
        }

        // Validar código
        public bool ValidateCode(string email, string code)
        {
            // 1) Validar con caché
            if (_cache.ValidateCode(email, code))
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var user = db.users.FirstOrDefault(u => u.email == email);

                if (user != null)
                {
                    user.Status = UserStatus.Active;
                    user.EmailVerified = true;
                    user.EmailVerifiedAt = DateTime.UtcNow;
                    db.SaveChanges();
                }

                return true;
            }

            // 2) Validar con BD
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var user = db.users.FirstOrDefault(u => u.email == email);

                if (user != null &&
                    user.EmailVerificationCode == code &&
                    user.EmailVerificationExpiresAt > DateTime.UtcNow)
                {
                    user.Status = UserStatus.Active;
                    user.EmailVerified = true;
                    user.EmailVerifiedAt = DateTime.UtcNow;
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public async Task SendEmailAsync(string email, VerificacionEmailBuilder builder)
        {
            var emailSender = _scopeFactory.GetRequiredService<IServiceEmail>();
            await emailSender.SendEmailAsync(email, builder.GetSubject(), builder.GetBody());
        }

        // Reactivar cuenta bloqueada
        public async Task<bool> ReactivateAccountAsync(string email, string code)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = db.users.FirstOrDefault(u => u.email == email);

            if (user != null &&
                user.Status == UserStatus.Blocked &&
                user.EmailVerificationCode == code &&
                user.EmailVerificationExpiresAt > DateTime.UtcNow)
            {
                user.Status = UserStatus.Active;
                user.EmailVerifiedAt = DateTime.UtcNow;
                await db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        // Enviar código de reactivación
        public async Task SendReactivationCodeAsync(string email)
        {
            var code = CodeGenerator.GenerateNumericCode();
            _cache.SaveCode(email, code, TimeSpan.FromHours(24));

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = db.users.FirstOrDefault(u => u.email == email);

            if (user != null && user.Status == UserStatus.Blocked)
            {
                user.EmailVerificationCode = code;
                user.EmailVerificationExpiresAt = DateTime.UtcNow.AddHours(24);
                await db.SaveChangesAsync();

                await _emailQueue.QueueBackgroundWorkItemAsync(async () =>
                {
                    var builder = new VerificacionEmailBuilder(user.Person?.firstName ?? "Usuario", code);
                    using var innerScope = _scopeFactory.CreateScope();
                    var emailSender = innerScope.ServiceProvider.GetRequiredService<IServiceEmail>();
                    await emailSender.SendEmailAsync(email, builder.GetSubject(), builder.GetBody());
                });
            }
        }

        // Re-verificación mensual
        public async Task SendMonthlyReverificationAsync(string email, string nombre)
        {
            var code = CodeGenerator.GenerateNumericCode();
            _cache.SaveCode(email, code, TimeSpan.FromDays(3));

            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var user = db.users.FirstOrDefault(u => u.email == email);

            if (user != null && user.Status == UserStatus.Active)
            {
                user.EmailVerificationCode = code;
                user.EmailVerificationExpiresAt = DateTime.UtcNow.AddDays(3);
                user.LastVerificationSentAt = DateTime.UtcNow;
                await db.SaveChangesAsync();

                await _emailQueue.QueueBackgroundWorkItemAsync(async () =>
                {
                    var builder = new VerificacionEmailBuilder(nombre, code);
                    using var innerScope = _scopeFactory.CreateScope();
                    var emailSender = innerScope.ServiceProvider.GetRequiredService<IServiceEmail>();
                    await emailSender.SendEmailAsync(email, builder.GetSubject(), builder.GetBody());
                });
            }
        }
    }
}
