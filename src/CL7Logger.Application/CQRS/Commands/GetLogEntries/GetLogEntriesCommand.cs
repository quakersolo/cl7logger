using CL7Logger.Application.CQRS.Common;
using CL7Logger.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Application.CQRS.Commands.GetLogEntries
{
    public class GetLogEntriesCommand : IRequest<GetLogEntriesResult>
    {
        private readonly ILoggerDbContext loggerDbContext;

        public GetLogEntriesCommand(ILoggerDbContext loggerDbContext)
        {
            this.loggerDbContext = loggerDbContext;
        }

        public GetLogEntriesParameters Parameters { get; set; }

        public Task<GetLogEntriesResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
