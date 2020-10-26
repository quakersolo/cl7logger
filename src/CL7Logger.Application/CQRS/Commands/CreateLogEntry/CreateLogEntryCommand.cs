using CL7Logger.Application.CQRS.Common;
using CL7Logger.Application.Interfaces;
using CL7Logger.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Application.CQRS.Commands.CreateLogEntry
{
    public class CreateLogEntryCommand : IRequest<Guid>, ICreateLogEntryCommand
    {
        private readonly IHttpContextAccessor httpContextAccesor;
        private readonly ILoggerDbContext loggerDbContext;
        private readonly LoggerOptions loggerOptions;

        public CreateLogEntryCommand(
            IHttpContextAccessor httpContextAccesor,
            ILoggerDbContext loggerDbContext,
            IOptions<LoggerOptions> loggerOptions)
        {
            this.httpContextAccesor = httpContextAccesor;
            this.loggerDbContext = loggerDbContext;
            this.loggerOptions = loggerOptions.Value;
        }

        public CreateLogEntryParameters Parameters { get; set; }

        public async Task<Guid> ExecuteAsync(CancellationToken cancellationToken)
        {
            LogEntry logEntry = new LogEntry
            {
                ApplicationName = loggerOptions.ApplicationName,
                TraceId = loggerOptions.TraceId ?? Guid.NewGuid(),
                LogLevel = Parameters.LogLevel,
                LogLevelName = Parameters.LogLevel.ToString(),
                Message = Parameters.Message,
                Detail = Parameters.Detail ?? JsonSerializer.Serialize(new { }),
                Host = string.Empty,
                User = string.Empty,
                CreatedAt = DateTime.UtcNow,
            };

            if (httpContextAccesor.HttpContext != null)
            {
                logEntry.Host = httpContextAccesor.HttpContext.Request.Host.Value ?? string.Empty;
                logEntry.User = httpContextAccesor.HttpContext.User?.Identity.Name ?? string.Empty;
            }

            loggerDbContext.LogEntries.Add(logEntry);

            await loggerDbContext.SaveChangesAsync(cancellationToken);

            return logEntry.Id;
        }
    }
}
