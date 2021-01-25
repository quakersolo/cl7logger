using System;

namespace CL7Logger.Common
{
    public class CLogInfo
    {
        public Guid TraceId { get; set; }
        public string UserId { get; set; }
        public string ApplicationName { get; set; }
        public string TraceIdHeaderName { get; set; } = "CLogTraceId";
    }
}
