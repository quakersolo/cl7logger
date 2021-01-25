using CLogger.Transport;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CLogger.APIExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ICLogMonitor logger;
        private readonly WeatherForecast weatherForecast;

        public WeatherForecastController(ICLogMonitor logger, WeatherForecast weatherForecast)
        {
            this.logger = logger;
            this.weatherForecast = weatherForecast;
        }

        [HttpGet]
        public async Task<string> GetAsync(CancellationToken cancellationToken)
        {
            await logger.AddInformationAsync("AddInformationAsync 1!");
            await logger.AddTraceAsync("AddTraceAsync 1!", cancellationToken);

            var rng = new Random();
            var stringtoreturn = await weatherForecast.Setup(Summaries[rng.Next(Summaries.Length)], cancellationToken);

            try
            {
                await logger.AddWarningAsync("Dividiremos entre zero!", cancellationToken);
                throw new DivideByZeroException();
            }
            catch (Exception e)
            {
                await logger.AddExceptionAsync(e, cancellationToken);
            }

            var res = await logger.ListLogsAsync(new ListLogsParameters
            {
                LogEntryType = CLogEntryType.All,
                TraceId = new Guid("0d348b5d-7fb6-43ae-9daf-58213358c39a")
            });

            return stringtoreturn;
        }
    }
}
