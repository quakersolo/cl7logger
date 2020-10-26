using System.Collections.Generic;

namespace CL7Logger.Application.CQRS.Commands.GetLogEntries
{
    public class GetLogEntriesResult
    {
        public IEnumerable<GetLogEntriesResultItem> Items { get; set; }
    }
}
