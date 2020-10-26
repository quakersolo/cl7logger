using Microsoft.Extensions.Logging;
using System;

namespace CL7Logger.Domain
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string ApplicationName { get; set; }
        public Guid TraceId { get; set; }
        public LogLevel LogLevel { get; set; }
        public string LogLevelName { get; set; }
        public string Host { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
