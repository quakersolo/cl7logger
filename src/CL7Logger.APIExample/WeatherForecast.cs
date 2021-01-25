using System.Threading;
using System.Threading.Tasks;

namespace CLogger.APIExample
{
    public class WeatherForecast
    {
        private readonly ICLogMonitor logManager;

        public WeatherForecast(ICLogMonitor logManager)
        {
            this.logManager = logManager;
        }

        public async Task<string> Setup(string summary, CancellationToken cancellationToken)
        {
            await logManager.AddTraceAsync("Setup: AddTraceAsync X?!", cancellationToken);
            return summary;
        }
    }
}
