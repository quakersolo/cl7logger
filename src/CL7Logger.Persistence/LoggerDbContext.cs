using CL7Logger.Application;
using CL7Logger.Application.Interfaces;
using CL7Logger.Domain;
using CL7Logger.Persistence.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Persistence
{
    public class LoggerDbContext : DbContext, ILoggerDbContext
    {
        private readonly LoggerOptions loggerOptions;

        public LoggerDbContext(DbContextOptions<LoggerDbContext> options, IOptions<LoggerOptions> loggerOptions)
        : base(options)
        {
            this.loggerOptions = loggerOptions.Value;
        }

        public DbSet<LogEntry> LogEntries { get; set; }

        public async Task EnsureDatabase(string connectionString, CancellationToken cancellationToken)
        {
            string queryString = LoggerSqlHelper.EnsureLogTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                await command.Connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(loggerOptions.ConnectionString);
            }
        }
    }
}
