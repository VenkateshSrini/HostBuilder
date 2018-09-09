using HoustBuilder.LoadData.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using HoustBuilder.LoadData.DbServices;

namespace HoustBuilder.LoadData.ServiceProviders
{
    internal class DataLoaderService : IHostedService
    {
        ILogger<DataLoaderService> _logger;
        string _connectionString;
        IDatabase _database;
        public DataLoaderService(ILogger<DataLoaderService>logger, IOptions<CloudFoundryServicesOptions> serviceOptions,
            IOptions<Dbconfig> dbConfig, IDatabase db)
        {
            //var credentials = serviceOptions.Value.ServicesList.FirstOrDefault<Service>((service) =>
            //(service.Name.CompareTo(dbConfig.Value.ServiceName) == 0)).Credentials;

            //if ((credentials != null) && (credentials.Any()))
            //    _connectionString = credentials["connectionString"].Value;
            _logger = logger;
            _database = db;


        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting the dbservice");
            _database.ExecuteScalar( "sp");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("stopping the dbservice");
            return Task.CompletedTask;
        }
    }
}
