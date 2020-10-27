using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.APIExample
{
    public class WeatherForecast
    {
        private readonly ILogManager logManager;

        public WeatherForecast(ILogManager logManager)
        {
            this.logManager = logManager;
        }

        public async Task<string> Setup(string summary, CancellationToken cancellationToken)
        {
            await logManager.AddLogAsync("Hola mundo Trace!", Common.Enums.LogEntryType.Trace, cancellationToken);
            return summary;
        }
    }
}
