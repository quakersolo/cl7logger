using System;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Application.CQRS.Commands.CreateLogEntry
{
    public interface ICreateLogEntryCommand
    {
        CreateLogEntryParameters Parameters { get; set; }

        Task<Guid> ExecuteAsync(CancellationToken cancellationToken);
    }
}