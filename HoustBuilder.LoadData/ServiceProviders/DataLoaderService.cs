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

namespace HoustBuilder.LoadData.ServiceProviders
{
    internal class DataLoaderService : IHostedService
    {
        ILogger<DataLoaderService> _logger;
        string connectionString;
        public DataLoaderService(ILogger<DataLoaderService>logger, IOptions<CloudFoundryServicesOptions> serviceOptions,
            IOptions<Dbconfig> dbConfig)
        {
            var credentials = serviceOptions.Value.ServicesList.FirstOrDefault<Service>((service) =>
            (service.Name.CompareTo(dbConfig.Value.ServiceName) == 0)).Credentials;

            if ((credentials != null) && (credentials.Any()))
                connectionString = credentials["connectionString"].Value;
            _logger = logger;


        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
