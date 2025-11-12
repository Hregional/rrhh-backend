
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; // Added
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using rrhh_backend.Data;
using rrhh_backend.Services.Administracion;

namespace rrhh_backend.Services.Hosted
{
    public class TareaProgramadaService : BackgroundService
    {
        private readonly ILogger<TareaProgramadaService> _logger;
        private readonly IServiceScopeFactory _scopeFactory; // Changed

        public TareaProgramadaService(ILogger<TareaProgramadaService> logger, IServiceScopeFactory scopeFactory) // Changed
        {
            _logger = logger;
            _scopeFactory = scopeFactory; // Changed
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tarea Programada Service is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation("Tarea Programada Service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var earlyMorningRun = new DateTime(now.Year, now.Month, now.Day, 14, 40, 0); // 01:00 AM
                var nightRun = new DateTime(now.Year, now.Month, now.Day, 21, 0, 0);      // 09:00 PM

                DateTime nextRunTime;

                if (now < earlyMorningRun)
                {
                    nextRunTime = earlyMorningRun;
                }
                else if (now < nightRun)
                {
                    nextRunTime = nightRun;
                }
                else
                {
                    // Next run is tomorrow morning
                    nextRunTime = earlyMorningRun.AddDays(1);
                }

                var delay = nextRunTime - now;

                _logger.LogInformation($"Next background task will run at: {nextRunTime}. Waiting for {delay.TotalHours:F2} hours.");
                
                try
                {
                    await Task.Delay(delay, stoppingToken);
                }
                catch (TaskCanceledException)
                {
                    // This is expected when the service is stopping.
                    break;
                }


                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                _logger.LogInformation($"Tarea Programada Service is doing background work at {DateTime.Now:HH:mm:ss}.");

                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<RrHhContext>();
                        var auditoriaService = scope.ServiceProvider.GetRequiredService<IAuditoriaService>();

                        // --- Lógica Unificada de Estados ---

                        // IDs de Estado (Confirmar desde la tabla RHEstadoColaborador)
                        var idEstadoActivo = 1;
                        var idEstadoPermiso = 2;
                        var idEstadoInactivo = 3; // Asumiendo que 3 es Inactivo/Baja
                        var idEstadoVacaciones = 4; // Asumiendo 4 es Vacaciones
                        var idEstadoAsueto = 5;       // NUEVO
                        var idEstadoFinDeSemana = 6;  // NUEVO

                        var idEstadoLicenciaAprobada = 2; // "Aprobada" en RHEstadoLicencias
                        var today = DateTime.Now.Date;

                        // Obtener datos de soporte
                        var asuetos = await context.RHAsuetos.Select(a => a.Fecha.Date).ToListAsync(stoppingToken);
                        var isHoliday = asuetos.Contains(today);
                        var isWeekend = today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday;

                        // Obtener todos los colaboradores cuyo estado necesita ser re-evaluado.
                        // Excluir a los que están inactivos permanentemente.
                        var colaboradoresParaReevaluar = await context.RHColaboradors
                            .Where(c => c.IdEstadoColaborador != idEstadoInactivo)
                            .ToListAsync(stoppingToken);

                        foreach (var colaborador in colaboradoresParaReevaluar)
                        {
                            var oldStatus = colaborador.IdEstadoColaborador;
                            int newStatus;
                            string changeReason;

                            // Prioridad 1: Licencia activa
                            var tieneLicenciaActiva = await context.RHLicencias.AnyAsync(l =>
                                l.IdColaborador == colaborador.IdColaborador &&
                                l.IdEstadoLicencia == idEstadoLicenciaAprobada &&
                                l.FechaInicio.Date <= today && l.FechaFin.Date >= today, stoppingToken);

                            if (tieneLicenciaActiva)
                            {
                                // Aquí se podría diferenciar entre tipo de licencia si fuera necesario (ej. Permiso vs Vacaciones)
                                newStatus = idEstadoPermiso; 
                                changeReason = "Inicio o continuación de Permiso/Licencia";
                            }
                            // Prioridad 2: Asueto
                            else if (isHoliday)
                            {
                                newStatus = idEstadoAsueto;
                                changeReason = "Asueto general";
                            }
                            // Prioridad 3: Fin de semana
                            else if (isWeekend)
                            {
                                newStatus = idEstadoFinDeSemana;
                                changeReason = "Fin de semana";
                            }
                            // Prioridad 4: Activo por defecto
                            else
                            {
                                newStatus = idEstadoActivo;
                                changeReason = "Día laboral activo";
                            }

                            // Si el estado calculado es diferente al actual, se actualiza.
                            if (oldStatus != newStatus)
                            {
                                colaborador.IdEstadoColaborador = newStatus;
                                await auditoriaService.RegistrarCambioEstatus(
                                    colaborador.IdColaborador,
                                    oldStatus,
                                    newStatus,
                                    changeReason,
                                    "TareaProgramadaService");
                                _logger.LogInformation($"Cambiando estado para colaborador {colaborador.IdColaborador} de {oldStatus} a {newStatus} ({changeReason})");
                            }
                        }

                        await context.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Verificación de estados de colaboradores completada.");
                        // --- Fin Lógica Unificada ---
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during the scheduled task. Will retry at the next scheduled time.");
                }
            }

            _logger.LogInformation("Tarea Programada Service has stopped.");
        }
    }
}
