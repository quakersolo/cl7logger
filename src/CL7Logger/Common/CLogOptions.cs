using Microsoft.AspNetCore.Http;
using System;

namespace CLogger
{
    public class CLogOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public Guid TraceId { get; set; }

        public string TraceIdHeaderName { get; set; } = "CL7TraceId";
        public string Path { get; set; } = "/Logentries";
    }
}
