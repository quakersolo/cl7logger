using CL7Logger.Entities;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Repositories
{
    internal class LogEntryRepository : BaseRepository<LogEntry>
    {
        public LogEntryRepository(string connectionString) : base(connectionString)
        {
        }

        public override async Task<Guid> AddAsync(LogEntry entity, CancellationToken cancellationToken = default)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Helpers.SqlCommandText.LogEntryInsert, conn);
                Guid entityId = Guid.NewGuid();

                cmd.Parameters.AddWithValue("@Id", entityId);
                cmd.Parameters.AddWithValue("@ApplicationName", entity.ApplicationName);
                cmd.Parameters.AddWithValue("@TraceId", entity.TraceId);
                cmd.Parameters.AddWithValue("@LogEntryType", (int)entity.LogEntryType);
                cmd.Parameters.AddWithValue("@LogEntryTypeName", entity.LogEntryType.ToString());
                cmd.Parameters.AddWithValue("@Message", entity.Message);
                cmd.Parameters.AddWithValue("@Detail", entity.Detail);
                cmd.Parameters.AddWithValue("@Host", entity.Host);
                cmd.Parameters.AddWithValue("@UserId", entity.UserId);
                cmd.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt);

                await conn.OpenAsync(cancellationToken);
                await cmd.ExecuteNonQueryAsync(cancellationToken);

                return entityId;
            }
        }
    }
}
