using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Core.Interfaces
{
    public interface ICL7Logger
    {
        Task<Guid> LogAsync(LogLevel logLevel, string message, CancellationToken cancellationToken);

        Task<Guid> LogErrorAsync(Exception exception, CancellationToken cancellationToken);
    }
}
