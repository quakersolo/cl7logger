using System;

namespace CL7Logger
{
    public class CL7LogOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public Guid TraceId { get; set; }

        public string TraceIdHeaderName { get; set; } = "CL7TraceId";
        public string Path { get; set; } = "/Logentries";
    }
}
