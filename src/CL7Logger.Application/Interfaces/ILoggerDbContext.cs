using CL7Logger.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Application.Interfaces
{
    public interface ILoggerDbContext
    {
        DbSet<LogEntry> LogEntries { get; set; }
        Task EnsureDatabase(string connectionString, CancellationToken cancellationToken);

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
