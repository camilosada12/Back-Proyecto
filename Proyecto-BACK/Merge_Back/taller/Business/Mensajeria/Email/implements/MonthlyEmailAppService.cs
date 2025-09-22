// Business.Mensajeria.Email.implements/MonthlyEmailAppService.cs
using Business.Mensajeria.Email.@interface;
using Data.Interfaces.IDataImplement.Security; // IUserRepository
using Entity.Domain.Enums;
using Entity.Domain.Models.Implements.ModelSecurity;
using Entity.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Business.Mensajeria.Email.implements
{
    public class MonthlyEmailAppService
{
    private readonly IServiceEmail _email;
    private readonly IUserRepository _users;
    private readonly ApplicationDbContext _db; // tu AppDbContext
    private readonly ILogger<MonthlyEmailAppService> _logger;

    public MonthlyEmailAppService(
        IServiceEmail email,
        IUserRepository users,
        ApplicationDbContext db, // inyecta AppDbContext
        ILogger<MonthlyEmailAppService> logger)
    {
        _email = email;
        _users = users;
        _db = db;
        _logger = logger;
    }

        // Se ejecuta el día 4 por el worker
        public async Task EjecutarEnvioMensualAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            // ⚡ Buscar usuarios activos
            var activos = await _db.users
                .Where(u => u.Status == UserStatus.Active)
                .ToListAsync(ct);

            foreach (var u in activos)
            {
                // Si ya le enviamos este mes, no repetir
                if (u.LastVerificationSentAt.HasValue && u.LastVerificationSentAt.Value.Month == now.Month)
                    continue;

                // Generar código nuevo válido 3 días
                var code = new Random().Next(100000, 999999).ToString();
                u.EmailVerificationCode = code;
                u.EmailVerificationExpiresAt = now.AddDays(3);
                u.LastVerificationSentAt = now;

                try
                {
                    var builder = new VerificacionEmailBuilder(
                        u.Person?.firstName ?? "Usuario",
                        code
                    );

                    await _email.SendEmailAsyncVerificacion(u.email!, builder);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error enviando código mensual a {Email}", u.email);
                }
            }

            await _db.SaveChangesAsync(ct);
        }

    }
}
