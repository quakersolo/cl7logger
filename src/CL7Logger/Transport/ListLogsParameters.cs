using CL7Logger.Common.Enums;
using System;
using System.Collections.Generic;

namespace CL7Logger.Transport
{
    public class ListLogsParameters
    {
        public Guid TraceId { get; set; } = default;
        public LogEntryType LogEntryType { get; set; } = LogEntryType.All;

        internal static IDictionary<string, object> ToDictionary(ListLogsParameters parameters)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            dictionary.Add("@TraceId", parameters.TraceId);
            dictionary.Add("@LogEntryType", (int)parameters.LogEntryType);

            return dictionary;
        }
    }
}
