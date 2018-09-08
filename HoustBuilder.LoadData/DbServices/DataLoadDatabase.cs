using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace HoustBuilder.LoadData.DbServices
{
    internal class DataLoadDatabase : IDatabase
    {
        ILogger<DataLoadDatabase> _logger;
        public DataLoadDatabase(ILogger<DataLoadDatabase> logger)
        {
            _logger = logger;
        }
        public void ExecuteScalar(string connectionString, string spCommand, params DbParameter[] parameters)
        {
            _logger.LogInformation($"connectionstring {connectionString}");
        }
    }
}
