using System;

namespace CLogger.Entities
{
    internal class LogEntry : BaseEntity
    {
        public string ApplicationName { get; set; }
        public Guid TraceId { get; set; }
        public CLogEntryType LogEntryType { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public string Host { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
