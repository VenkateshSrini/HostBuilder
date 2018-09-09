using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace HoustBuilder.LoadData.DbServices
{
    internal class DataLoadDatabase : IDatabase
    {
        ILogger<DataLoadDatabase> _logger;
        IDbConnection _connection;
        public DataLoadDatabase(ILogger<DataLoadDatabase> logger, IDbConnection dbConnection)
        {
            _logger = logger;
            _connection = dbConnection;
            
        }
        public void ExecuteScalar(string spCommand, params DbParameter[] parameters)
        {

            _logger.LogInformation($"connectionstring {_connection.ConnectionString}");
        }
    }
}
