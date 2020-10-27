using CL7Logger.Common.Enums;
using CL7Logger.Entities;
using CL7Logger.Repositories;
using CL7Logger.Transport;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public async Task<ListLogsResult> ListLogsAsync(ListLogsParameters parameters, CancellationToken cancellationToken = default)
        {
            LogEntryRepository logEntryRepository = new LogEntryRepository(options.Value.ConnectionString);

            IEnumerable<LogEntry> logEntries = await logEntryRepository.ListAsync(ListLogsParameters.ToDictionary(parameters), cancellationToken);

            return ListLogsResult.FromLogEntries(logEntries);
        }

        public async Task<Guid> AddLogAsync(string message, LogEntryType logEntryType = LogEntryType.Trace, CancellationToken cancellationToken = default)
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

        public async Task<Guid> AddLogAsync(string message, CancellationToken cancellationToken)
        {
            return await AddLogAsync(message, LogEntryType.Trace, cancellationToken);
        }

        public async Task<Guid> AddLogErrorAsync(Exception exception, CancellationToken cancellationToken = default)
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
