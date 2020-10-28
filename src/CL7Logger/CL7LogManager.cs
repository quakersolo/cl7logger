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
    public class CL7LogManager : ICL7LogManager
    {
        private readonly IOptions<CL7LogOptions> options;

        public CL7LogManager(IOptions<CL7LogOptions> options)
        {
            this.options = options;
        }

        public Task<Guid> AddTraceAsync(string message, CancellationToken cancellationToken = default)
        {
            return AddLogAsync(message, CL7LogEntryType.Trace, cancellationToken);
        }

        public Task<Guid> AddInformationAsync(string message, CancellationToken cancellationToken = default)
        {
            return AddLogAsync(message, CL7LogEntryType.Information, cancellationToken);
        }

        public Task<Guid> AddWarningAsync(string message, CancellationToken cancellationToken = default)
        {
            return AddLogAsync(message, CL7LogEntryType.Warning, cancellationToken);
        }

        public async Task<Guid> AddExceptionAsync(Exception exception, CancellationToken cancellationToken = default)
        {
            LogEntry logEntry = new LogEntry
            {
                ApplicationName = options.Value.ApplicationName,
                TraceId = options.Value.TraceId,
                LogEntryType = CL7LogEntryType.Error,
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

        public async Task<ListLogsResult> ListLogsAsync(ListLogsParameters parameters, CancellationToken cancellationToken = default)
        {
            LogEntryRepository logEntryRepository = new LogEntryRepository(options.Value.ConnectionString);

            IEnumerable<LogEntry> logEntries = await logEntryRepository.ListAsync(ListLogsParameters.ToDictionary(parameters), cancellationToken);

            return ListLogsResult.FromLogEntries(logEntries);
        }

        private async Task<Guid> AddLogAsync(string message, CL7LogEntryType logEntryType, CancellationToken cancellationToken)
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
    }
}
