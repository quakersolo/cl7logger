using CLogger.Entities;
using CLogger.Repositories;
using CLogger.Transport;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CLogger
{
    public class CLogMonitor : ICLogMonitor
    {
        private readonly CLogOptions options;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CLogMonitor(IOptions<CLogOptions> options, IHttpContextAccessor httpContextAccessor)
        {
            this.options = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<Guid> AddTraceAsync(string message, CancellationToken cancellationToken = default)
        {
            return AddLogAsync(message, CLogEntryType.Trace, cancellationToken);
        }

        public Task<Guid> AddInformationAsync(string message, CancellationToken cancellationToken = default)
        {
            return AddLogAsync(message, CLogEntryType.Information, cancellationToken);
        }

        public Task<Guid> AddWarningAsync(string message, CancellationToken cancellationToken = default)
        {
            return AddLogAsync(message, CLogEntryType.Warning, cancellationToken);
        }

        public async Task<Guid> AddExceptionAsync(Exception exception, CancellationToken cancellationToken = default)
        {
            LogEntry logEntry = new LogEntry
            {
                ApplicationName = options.LogginInfo.ApplicationName,
                TraceId = options.LogginInfo.TraceId,
                LogEntryType = CLogEntryType.Error,
                Message = exception.Message,
                Detail = JsonSerializer.Serialize(new
                {
                    Type = exception.GetType().Name,
                    exception.StackTrace
                }),
                Host = httpContextAccessor.HttpContext?.Request.Host.Host ?? string.Empty,
                UserId = httpContextAccessor.HttpContext?.User.Identity.Name ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
            };

            LogEntryRepository logEntryRepository = new LogEntryRepository(options.ConnectionString);
            return await logEntryRepository.AddAsync(logEntry, cancellationToken);
        }

        public async Task<ListLogsResult> ListLogsAsync(ListLogsParameters parameters, CancellationToken cancellationToken = default)
        {
            LogEntryRepository logEntryRepository = new LogEntryRepository(options.ConnectionString);

            IEnumerable<LogEntry> logEntries = await logEntryRepository.ListAsync(ListLogsParameters.ToDictionary(parameters, options.LogginInfo.ApplicationName), cancellationToken);

            return ListLogsResult.FromLogEntries(logEntries);
        }

        private async Task<Guid> AddLogAsync(string message, CLogEntryType logEntryType, CancellationToken cancellationToken)
        {
            LogEntry logEntry = new LogEntry
            {
                ApplicationName = options.LogginInfo.ApplicationName,
                TraceId = options.LogginInfo.TraceId,
                LogEntryType = logEntryType,
                Message = message,
                Detail = JsonSerializer.Serialize(new { }),
                Host = httpContextAccessor.HttpContext?.Request.Host.Host ?? string.Empty,
                UserId = options.LogginInfo.UserId ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
            };

            LogEntryRepository logEntryRepository = new LogEntryRepository(options.ConnectionString);
            return await logEntryRepository.AddAsync(logEntry, cancellationToken);
        }
    }
}
