using CLogger.Transport;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CLogger
{
    public interface ICLogMonitor
    {
        Task<Guid> AddTraceAsync(string message, CancellationToken cancellationToken = default);
        Task<Guid> AddInformationAsync(string message, CancellationToken cancellationToken = default);
        Task<Guid> AddWarningAsync(string message, CancellationToken cancellationToken = default);
        Task<Guid> AddExceptionAsync(Exception exception, CancellationToken cancellationToken = default);

        Task<ListLogsResult> ListLogsAsync(ListLogsParameters parameters, CancellationToken cancellationToken = default);
    }
}
