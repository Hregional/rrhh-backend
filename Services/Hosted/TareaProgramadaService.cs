
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace rrhh_backend.Services.Hosted
{
    public class TareaProgramadaService : BackgroundService
    {
        private readonly ILogger<TareaProgramadaService> _logger;

        public TareaProgramadaService(ILogger<TareaProgramadaService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Tarea Programada Service is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation("Tarea Programada Service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var currentHour = now.Hour;

                // Define the allowed time windows (00:00-04:00 and 20:00-24:00)
                bool isWithinAllowedTime =
                    (currentHour >= 0 && currentHour < 4) || // 00:00, 01:00, 02:00, 03:00
                    (currentHour >= 20 && currentHour < 24); // 20:00, 21:00, 22:00, 23:00

                if (isWithinAllowedTime)
                {
                    _logger.LogInformation($"Tarea Programada Service is doing background work at {now:HH:mm:ss}.");

                    // Aquí puedes poner la lógica que quieres que se ejecute en cada iteración.
                    // Por ejemplo, llamar a otro servicio, hacer una limpieza, etc.

                    // After performing the task, wait for a short interval before checking again
                    // This prevents a tight loop within the allowed window
                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Check every 5 minutes within the window
                }
                else
                {
                    _logger.LogInformation($"Tarea Programada Service is currently outside allowed hours ({now:HH:mm:ss}). Waiting to re-check at the next hour.");

                    // Calculate delay until the start of the next hour
                    var nextHour = now.AddHours(1);
                    var nextHourStart = new DateTime(nextHour.Year, nextHour.Month, nextHour.Day, nextHour.Hour, 0, 0);
                    var delay = nextHourStart - now;

                    // Ensure minimum delay if it's very close to the hour mark
                    if (delay.TotalMilliseconds < 1000)
                    {
                        delay = TimeSpan.FromSeconds(1);
                    }

                    _logger.LogInformation($"Waiting for {delay.TotalMinutes:F2} minutes until {nextHourStart:HH:mm:ss} to re-check.");
                    await Task.Delay(delay, stoppingToken);
                }
            }

            _logger.LogInformation("Tarea Programada Service has stopped.");
        }
    }
}
