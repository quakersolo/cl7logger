using System;

namespace CL7Logger.Application
{
    public class LoggerOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public string TraceIdHeaderName { get; set; } = "CL7TraceId";
        public Guid? TraceId { get; set; }
    }
}
