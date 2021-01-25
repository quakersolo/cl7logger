using CLogger.Repositories.Helpers;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CLogger.Common
{
    internal class DatabaseManager
    {
        public static async Task CreateTableIfNotExists(string connectionString, CancellationToken cancellationToken = default)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(SqlCommandText.CreateTableIfNotExists, connection);
                await command.Connection.OpenAsync(cancellationToken);
                await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }
}
