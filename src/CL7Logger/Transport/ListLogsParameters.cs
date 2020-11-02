using System;
using System.Collections.Generic;

namespace CL7Logger.Transport
{
    public class ListLogsParameters
    {
        public Guid LogEntryId { get; set; } = default;
        public Guid TraceId { get; set; } = default;
        public CL7LogEntryType LogEntryType { get; set; } = CL7LogEntryType.All;

        internal static IDictionary<string, object> ToDictionary(ListLogsParameters parameters, string applicationName)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            dictionary.Add("@ApplicationName", applicationName);
            dictionary.Add("@LogEntryId", parameters.LogEntryId);
            dictionary.Add("@TraceId", parameters.TraceId);
            dictionary.Add("@LogEntryType", (int)parameters.LogEntryType);

            return dictionary;
        }
    }
}
