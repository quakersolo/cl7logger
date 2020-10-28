using CL7Logger.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public override async Task<IEnumerable<LogEntry>> ListAsync(IDictionary<string, object> parameters, CancellationToken cancellationToken)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(Helpers.SqlCommandText.LogEntryList, conn);

                parameters.Keys.ToList().ForEach(key =>
                {
                    cmd.Parameters.AddWithValue(key, parameters[key]);
                });

                await conn.OpenAsync(cancellationToken);
                SqlDataReader reader = await cmd.ExecuteReaderAsync(cancellationToken);

                List<LogEntry> logEntries = new List<LogEntry>();
                while (await reader.ReadAsync(cancellationToken))
                {
                    logEntries.Add(FromSqlDataReaderToLogEntry(reader));
                }

                return logEntries;
            }
        }

        private LogEntry FromSqlDataReaderToLogEntry(SqlDataReader reader)
        {
            return new LogEntry
            {
                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                ApplicationName = reader.GetString(reader.GetOrdinal("ApplicationName")),
                TraceId = reader.GetGuid(reader.GetOrdinal("TraceId")),
                LogEntryType = (CL7LogEntryType)reader.GetInt32(reader.GetOrdinal("LogEntryType")),
                Message = reader.GetString(reader.GetOrdinal("Message")),
                Detail = reader.GetString(reader.GetOrdinal("Detail")),
                Host = reader.GetString(reader.GetOrdinal("Host")),
                UserId = reader.GetString(reader.GetOrdinal("UserId")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            };
        }
    }
}
