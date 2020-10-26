using CL7Logger.Application.CQRS.Commands.CreateLogEntry;
using CL7Logger.Core.Interfaces;
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

        public async Task<Guid> LogAsync(LogLevel logLevel, string message, CancellationToken cancellationToken)
        {
            createLogEntryCommand.Parameters = new CreateLogEntryParameters
            {
                LogLevel = logLevel,
                Message = message,
                Detail = null
            };

            return await createLogEntryCommand.ExecuteAsync(cancellationToken);
        }

        public async Task<Guid> LogErrorAsync(Exception exception, CancellationToken cancellationToken)
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
