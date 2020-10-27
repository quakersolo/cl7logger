using CL7Logger.Repositories.Helpers;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CL7Logger.Common
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
