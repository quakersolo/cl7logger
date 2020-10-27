using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.APIExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogManager logger;
        private readonly WeatherForecast weatherForecast;

        public WeatherForecastController(ILogManager logger, WeatherForecast weatherForecast)
        {
            this.logger = logger;
            this.weatherForecast = weatherForecast;
        }

        [HttpGet]
        public async Task<string> GetAsync(CancellationToken cancellationToken)
        {
            await logger.LogAsync("Hola mundo 1!");
            await logger.LogAsync("Hola mundo trace!", Common.Enums.LogEntryType.Trace);

            var rng = new Random();
            var stringtoreturn = await weatherForecast.Setup(Summaries[rng.Next(Summaries.Length)], cancellationToken);

            try
            {

                await logger.LogAsync("Dividiremos entre zero!", Common.Enums.LogEntryType.Information, cancellationToken);
                throw new DivideByZeroException();
            }
            catch (Exception e)
            {
                await logger.LogErrorAsync(e, cancellationToken);
            }

            return stringtoreturn;
        }
    }
}
