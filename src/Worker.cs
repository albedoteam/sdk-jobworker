using System;
using System.Linq;
using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AlbedoTeam.Sdk.JobWorker
{
    public class Worker : IWorkerRunner
    {
        private static Worker _instance;

        private Worker()
        {
        }

        private IHostBuilder HostBuilder { get; set; }

        private static Worker Instance => _instance ??= new Worker();

        public void Run()
        {
            HostBuilder.Build().Run();
        }

        public static Worker Configure<T>() where T : class, IWorkerConfigurator
        {
            var startup = Activator.CreateInstance<T>();
            Instance.HostBuilder = CreateHostBuilder<T>(startup);

            return Instance;
        }

        private static IHostBuilder CreateHostBuilder<T>(IWorkerConfigurator startup) where T : class
        {
            return Host.CreateDefaultBuilder()
                .UseSystemd()
                .ConfigureAppConfiguration((hostContext, builder) =>
                {
                    builder.AddUserSecrets<T>();
                })
                .UseSerilog((hostContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    if (services.All(s => s.ImplementationType != typeof(IJobRunner)))
                        services.AddTransient<IJobRunner, ShrugJobRunner>();

                    startup.Configure(services, hostContext.Configuration);
                    services.AddHostedService<BackgroundJob>();
                });
        }
    }
}