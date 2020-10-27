using CL7Logger.Application.CQRS.Commands.CreateLogEntry;
using CL7Logger.Core.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Core
{
    public class CL7Logger : ICL7Logger
    {
        private readonly ICreateLogEntryCommand createLogEntryCommand;

        public CL7Logger(ICreateLogEntryCommand createLogEntryCommand)
        {
            this.createLogEntryCommand = createLogEntryCommand;
        }

        /// <summary>
        /// Register a LogEntry in the database
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel">Loglevel is Information by default, you can change it at needed.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>LogEntryId genereated in the database.</returns>
        public async Task<Guid> LogAsync(string message, LogLevel logLevel = LogLevel.Information, CancellationToken cancellationToken = default)
        {
            createLogEntryCommand.Parameters = new CreateLogEntryParameters
            {
                LogLevel = logLevel,
                Message = message,
                Detail = null
            };

            return await createLogEntryCommand.ExecuteAsync(cancellationToken);
        }

        /// <summary>
        /// Register a LogEntry in the database by an Error/Exception
        /// </summary>
        /// <param name="exception">Exception thrown</param>
        /// <param name="cancellationToken"></param>
        /// <returns>LogEntryId genereated in the database.</returns>
        public async Task<Guid> LogErrorAsync(Exception exception, CancellationToken cancellationToken = default)
        {
            createLogEntryCommand.Parameters = new CreateLogEntryParameters
            {
                LogLevel = LogLevel.Error,
                Message = exception.Message,
                Detail = JsonSerializer.Serialize(new
                {
                    Type = exception.GetType().Name,
                    exception.StackTrace
                })
            };

            return await createLogEntryCommand.ExecuteAsync(cancellationToken);
        }

    }
}
