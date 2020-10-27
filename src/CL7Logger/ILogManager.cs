using CL7Logger.Common.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger
{
    public interface ILogManager
    {
        Task<Guid> LogAsync(string message, LogEntryType logEntryType = LogEntryType.Trace, CancellationToken cancellationToken = default);
        Task<Guid> LogAsync(string message, CancellationToken cancellationToken);

        Task<Guid> LogErrorAsync(Exception exception, CancellationToken cancellationToken = default);
    }
}
