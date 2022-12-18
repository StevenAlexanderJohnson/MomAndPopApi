using MySqlConnector;
using System.Data;

namespace Api.Dependencies
{
    public class MySqlConnectionFactory
    {
        private readonly string _connectionString;
        public MySqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<MySqlConnection> CreateConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }
    }
}
