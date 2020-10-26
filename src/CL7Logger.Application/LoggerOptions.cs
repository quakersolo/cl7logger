using System;

namespace CL7Logger.Application
{
    public class LoggerOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public Guid? TraceId { get; set; }
    }
}
