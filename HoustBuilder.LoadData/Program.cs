﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using HoustBuilder.LoadData.Configuration;
using HoustBuilder.LoadData.ServiceProviders;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System.Threading.Tasks;
using HoustBuilder.LoadData.DbServices;

namespace HoustBuilder.LoadData
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
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
                                  
                                   services.AddOptions();
                                   services.Configure<Dbconfig>(hostContext.Configuration.GetSection("DbConfig"));
                                   services.AddSingleton<IDatabase, DataLoadDatabase>();
                                   services.AddHostedService<DataLoaderService>();

                                   services.AddSingleton<IHostedService, DataLoaderService>();
                               })
                               .ConfigureLogging((hostingContext, logging) =>
                               {
                                   logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                                   logging.AddConsole();
                               });

            await builder.RunConsoleAsync();
          
            

        }
    }
}
