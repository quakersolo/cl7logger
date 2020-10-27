using CL7Logger.Core.Common.Interfaces;
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
        private readonly ICL7Logger logger;
        private readonly WeatherForecast weatherForecast;

        public WeatherForecastController(ICL7Logger logger, WeatherForecast weatherForecast)
        {
            this.logger = logger;
            this.weatherForecast = weatherForecast;
        }

        [HttpGet]
        public async Task<string> GetAsync(CancellationToken cancellationToken)
        {
            await logger.LogAsync("Hola mundo 1!");
            await logger.LogAsync("Hola mundo trace!", LogLevel.Trace);

            var rng = new Random();
            var stringtoreturn = await weatherForecast.Setup(Summaries[rng.Next(Summaries.Length)], cancellationToken);

            try
            {

                await logger.LogAsync("Dividiremos entre zero!", LogLevel.Warning, cancellationToken);
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
