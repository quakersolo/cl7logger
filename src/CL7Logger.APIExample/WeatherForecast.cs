using CL7Logger.Core.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.APIExample
{
    public class WeatherForecast
    {
        private readonly ICL7Logger logger;

        public WeatherForecast(ICL7Logger logger)
        {
            this.logger = logger;
        }

        public async Task<string> Setup(string summary, CancellationToken cancellationToken)
        {
            await logger.LogAsync("Hola mundo debug!", LogLevel.Debug, cancellationToken);
            return summary;
        }
    }
}
