using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Application.CQRS.Common
{
    public interface IRequest<T>
    {
        Task<T> ExecuteAsync(CancellationToken cancellationToken);
    }
}
