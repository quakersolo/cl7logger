using Microsoft.Extensions.Logging;

namespace CL7Logger.Application.CQRS.Commands.CreateLogEntry
{
    public class CreateLogEntryParameters
    {
        public string Message { get; set; }
        public string Detail { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
