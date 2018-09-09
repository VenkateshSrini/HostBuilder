using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;

namespace HoustBuilder.LoadData.DbServices
{
    internal class DataLoadDatabase : IDatabase
    {
        ILogger<DataLoadDatabase> _logger;
        DbConnection _connection;
        public DataLoadDatabase(ILogger<DataLoadDatabase> logger, IDbConnection dbConnection)
        {
            _logger = logger;
            _connection = dbConnection as DbConnection;
            
        }
        public void ExecuteScalar(string spCommand, CommandType typeOfCommand, params DbParameter[] parameters)
        {

            _logger.LogInformation($"Inside ExecuteScalar :- ConnectionString {_connection.ConnectionString}");
            using (var SqlCommand = _connection.CreateCommand())
            {
                SqlCommand.CommandText = spCommand;
                SqlCommand.CommandType = typeOfCommand;
                if ((parameters != null) && (parameters.Any()))
                    SqlCommand.Parameters.AddRange(parameters);
               // SqlCommand.ExecuteScalar();
            }

        }
    }
}
