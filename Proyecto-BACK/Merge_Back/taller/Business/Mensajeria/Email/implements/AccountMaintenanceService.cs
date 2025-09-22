using Entity.Domain.Enums;                // UserStatus
using Entity.Infrastructure.Contexts;     // ApplicationDbContext
using Microsoft.EntityFrameworkCore;      // EF Core
using Microsoft.Extensions.Logging;       // Logging
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Mensajeria.Email.implements
{
    /// <summary>
    /// Servicio de mantenimiento automático de cuentas:
    ///  - Bloquea usuarios que no verificaron en el plazo definido.
    ///  - Limpia cuentas pendientes caducadas.
    /// </summary>
    public class AccountMaintenanceService
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AccountMaintenanceService> _logger;

        public AccountMaintenanceService(ApplicationDbContext db, ILogger<AccountMaintenanceService> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Bloquea usuarios activos que no verificaron su correo en el plazo (ej: 3 días después del envío mensual).
        /// </summary>
        public async Task BloquearUsuariosNoVerificadosAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            var usuarios = await _db.users
                .Where(u => u.Status == UserStatus.Active
                         && u.EmailVerificationExpiresAt.HasValue
                         && u.EmailVerificationExpiresAt.Value < now)
                .ToListAsync(ct);

            foreach (var u in usuarios)
            {
                u.Status = UserStatus.Blocked;
            }

            if (usuarios.Count > 0)
            {
                await _db.SaveChangesAsync(ct);
                _logger.LogInformation("🔒 {Count} usuarios bloqueados por no verificar a tiempo.", usuarios.Count);
            }
            else
            {
                _logger.LogInformation("✅ Ningún usuario para bloquear.");
            }
        }

        /// <summary>
        /// Elimina o desactiva cuentas que quedaron en estado Pending y ya expiró el código inicial (ej: 24h).
        /// </summary>
        public async Task BloquearPendientesCaducadosAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            var pendientes = await _db.users
                .Where(u => u.Status == UserStatus.Pending
                         && u.EmailVerificationExpiresAt.HasValue
                         && u.EmailVerificationExpiresAt.Value < now)
                .ToListAsync(ct);

            if (pendientes.Count > 0)
            {
                foreach (var u in pendientes)
                {
                    u.Status = UserStatus.Blocked;
                }

                await _db.SaveChangesAsync(ct);
                _logger.LogInformation("🔒 {Count} cuentas pendientes bloqueadas por caducidad.", pendientes.Count);
            }
            else
            {
                _logger.LogInformation("✅ Ningún usuario pendiente caducado.");
            }
        }
    }
}
