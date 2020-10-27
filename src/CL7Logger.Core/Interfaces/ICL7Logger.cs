using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Core.Interfaces
{
    public interface ICL7Logger
    {
        Task<Guid> LogAsync(string message, LogLevel logLevel = LogLevel.Information, CancellationToken cancellationToken = default);

        Task<Guid> LogErrorAsync(Exception exception, CancellationToken cancellationToken = default);
    }
}
