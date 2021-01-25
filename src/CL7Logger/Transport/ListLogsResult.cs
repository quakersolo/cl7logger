using CLogger.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CLogger.Transport
{
    public class ListLogsResult
    {
        public IEnumerable<ListLogsResultItem> Items { get; set; }

        internal static ListLogsResult FromLogEntries(IEnumerable<LogEntry> logEntries)
        {
            return new ListLogsResult
            {
                Items = logEntries.Select(entry => new ListLogsResultItem
                {
                    Id = entry.Id,
                    ApplicationName = entry.ApplicationName,
                    TraceId = entry.TraceId,
                    LogEntryType = entry.LogEntryType,
                    Message = entry.Message,
                    Detail = entry.Detail,
                    Host = entry.Host,
                    UserId = entry.UserId,
                    CreatedAt = entry.CreatedAt
                })
            };
        }
    }
}
