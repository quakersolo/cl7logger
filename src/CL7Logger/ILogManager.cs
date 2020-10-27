using CL7Logger.Common.Enums;
using CL7Logger.Transport;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger
{
    public interface ILogManager
    {
        Task<Guid> AddLogAsync(string message, LogEntryType logEntryType = LogEntryType.Trace, CancellationToken cancellationToken = default);
        Task<Guid> AddLogAsync(string message, CancellationToken cancellationToken);
        Task<Guid> AddLogErrorAsync(Exception exception, CancellationToken cancellationToken = default);

        Task<ListLogsResult> ListLogsAsync(ListLogsParameters parameters, CancellationToken cancellationToken = default);
    }
}
