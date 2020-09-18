using System;
using JobWorkerSdk.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JobWorkerSdk
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
            Instance.HostBuilder = CreateHostBuilder(startup);
            
            return Instance;
        }

        private static IHostBuilder CreateHostBuilder(IWorkerConfigurator startup)
        {
            return Host.CreateDefaultBuilder()
                .UseWindowsService()
                .UseSerilog((hostContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostContext.Configuration))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IJobRunner, FakeJobRunner>();
                    
                    startup.Configure(services, hostContext.Configuration);
                    services.AddHostedService<BackgroundJob>();
                });
        }
    }
}