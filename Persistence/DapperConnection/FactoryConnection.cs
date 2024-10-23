using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Persistence.DapperConnection
{
    public class FactoryConnection : IFactioryConnection
    {
        private IDbConnection _connection;
        private readonly IOptions<ConnectionCFG> _configs;
        public FactoryConnection(IDbConnection connectionString){
            _connection = connectionString;
        }
        public void CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Open){
                _connection.Close();
            }
        }

        public IDbConnection GetConnection()
        {
            if (_connection == null) {
                _connection = new SqlConnection(_configs.Value.ConnectionSQL);
            }
            if (_connection.State == ConnectionState.Open){
                _connection.Open();
            }
            return _connection;
        }
    }
}