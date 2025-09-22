using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mensajeria.Email.implements
{
    /// <summary>
    /// Worker en segundo plano que ejecuta mantenimiento de cuentas cada noche.
    /// </summary>
    public class MaintenanceBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MaintenanceBackgroundService> _logger;

        public MaintenanceBackgroundService(IServiceProvider serviceProvider, ILogger<MaintenanceBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("🛠️ MaintenanceBackgroundService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Calcular cuánto falta hasta la próxima ejecución (2:00 am)
                    var delay = TiempoHastaSiguienteEjecucion();
                    await Task.Delay(delay, stoppingToken);

                    using var scope = _serviceProvider.CreateScope();
                    var maintenance = scope.ServiceProvider.GetRequiredService<AccountMaintenanceService>();

                    await maintenance.BloquearUsuariosNoVerificadosAsync(stoppingToken);
                    await maintenance.BloquearPendientesCaducadosAsync(stoppingToken);

                    _logger.LogInformation("✅ Mantenimiento nocturno ejecutado.");
                }
                catch (TaskCanceledException) { /* apagando */ }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "❌ Error en MaintenanceBackgroundService.");
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // backoff
                }
            }
        }

        private static TimeSpan TiempoHastaSiguienteEjecucion()
        {
            var now = DateTime.Now;
            var next = new DateTime(now.Year, now.Month, now.Day, 2, 0, 0);

            if (now >= next) next = next.AddDays(1);

            return next - now;
        }
    }
}
