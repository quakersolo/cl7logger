using System;

namespace CL7Logger
{
    public class LogManagerOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public Guid TraceId { get; set; }
    }
}
