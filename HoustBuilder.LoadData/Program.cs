﻿using HoustBuilder.LoadData.Configuration;
using HoustBuilder.LoadData.DbServices;
using HoustBuilder.LoadData.ServiceProviders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.CloudFoundry.Connector.SqlServer;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Threading.Tasks;

namespace HoustBuilder.LoadData
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                               .ConfigureAppConfiguration((hostingContext, config) =>
                               {
                                   config.AddJsonFile("appSettings.json", optional: true);
                                   config.AddEnvironmentVariables();
                                   config.AddCloudFoundry();
                                   if (args != null)
                                   {
                                       config.AddCommandLine(args);
                                   }


                               })
                               .ConfigureServices((hostContext, services) =>
                               {
                                   //var configRoot = hostContext.Configuration as IConfigurationRoot;
                                  services.AddSqlServerConnection(hostContext.Configuration);
                                   services.Configure<Dbconfig>(hostContext.Configuration.GetSection("DbConfig"));
                                   services.AddSingleton<IDatabase, DataLoadDatabase>();
                                   services.AddHostedService<DataLoaderService>();

                                   services.AddSingleton<IHostedService, DataLoaderService>();
                               })
                               .ConfigureLogging((hostingContext, logging) =>
                               {
                                   logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                   logging.AddConsole();
                               })
                               .UseConsoleLifetime()
                               .Build();
            await host.RunAsync();

           
          
            

        }
    }
}
