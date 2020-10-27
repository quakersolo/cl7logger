using CL7Logger.Common.Enums;
using CL7Logger.Entities;
using CL7Logger.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger
{
    public class LogManager : ILogManager
    {
        private readonly IOptions<LogManagerOptions> options;

        public LogManager(IOptions<LogManagerOptions> options)
        {
            this.options = options;
        }

        public async Task<Guid> LogAsync(string message, LogEntryType logEntryType = LogEntryType.Trace, CancellationToken cancellationToken = default)
        {
            LogEntry logEntry = new LogEntry
            {
                ApplicationName = options.Value.ApplicationName,
                TraceId = options.Value.TraceId,
                LogEntryType = logEntryType,
                Message = message,
                Detail = "{}",
                Host = string.Empty,
                UserId = string.Empty,
                CreatedAt = DateTime.UtcNow,
            };

            LogEntryRepository logEntryRepository = new LogEntryRepository(options.Value.ConnectionString);
            return await logEntryRepository.AddAsync(logEntry, cancellationToken);
        }

        public async Task<Guid> LogAsync(string message, CancellationToken cancellationToken)
        {
            return await LogAsync(message, LogEntryType.Trace, cancellationToken);
        }

        public async Task<Guid> LogErrorAsync(Exception exception, CancellationToken cancellationToken = default)
        {
            LogEntry logEntry = new LogEntry
            {
                ApplicationName = options.Value.ApplicationName,
                TraceId = options.Value.TraceId,
                LogEntryType = LogEntryType.Error,
                Message = exception.Message,
                Detail = JsonSerializer.Serialize(new
                {
                    Type = exception.GetType().Name,
                    exception.StackTrace
                }),
                Host = string.Empty,
                UserId = string.Empty,
                CreatedAt = DateTime.UtcNow,
            };

            LogEntryRepository logEntryRepository = new LogEntryRepository(options.Value.ConnectionString);
            return await logEntryRepository.AddAsync(logEntry, cancellationToken);
        }
    }
}
