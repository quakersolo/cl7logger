using System;

namespace CL7Logger
{
    public class CL7LogOptions
    {
        public string ApplicationName { get; set; }
        public string ConnectionString { get; set; }
        public Guid TraceId { get; set; }

        /// <summary>
        /// Name of the TraceId parameter in the RequestHeader collection. Default value: CL7TraceId
        /// </summary>
        public string TraceIdHeaderName { get; set; } = "CL7TraceId";
    }
}
