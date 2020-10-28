using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.APIExample
{
    public class WeatherForecast
    {
        private readonly ICL7LogManager logManager;

        public WeatherForecast(ICL7LogManager logManager)
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
