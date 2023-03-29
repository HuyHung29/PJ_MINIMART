using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace MINIMART.DL.Context
{
    public class DapperContext : IContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            _connectionString = _configuration.GetConnectionString("MySqlConnection");
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}
