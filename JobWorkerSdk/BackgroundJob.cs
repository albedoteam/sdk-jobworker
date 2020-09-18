using System;
using System.Threading;
using System.Threading.Tasks;
using JobWorkerSdk.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JobWorkerSdk
{
    internal class BackgroundJob : BackgroundService
    {
        private readonly IJobRunner _jobRunner;
        private readonly ILogger<BackgroundJob> _logger;

        public BackgroundJob(ILogger<BackgroundJob> logger, IJobRunner jobRunner)
        {
            _logger = logger;
            _jobRunner = jobRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _jobRunner.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker is running, but it does nothing ¯\\_(ツ)_/¯");
                await Task.Delay(10000, stoppingToken);
            }

            await _jobRunner.StopAsync(stoppingToken);
        }
    }
}