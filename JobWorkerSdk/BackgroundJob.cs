using System;
using System.Threading;
using System.Threading.Tasks;
using JobWorkerSdk.Configuration.Abstractions;
using Microsoft.Extensions.Hosting;

namespace JobWorkerSdk
{
    internal class BackgroundJob : BackgroundService
    {
        private readonly IJobRunner _jobRunner;

        public BackgroundJob(IJobRunner jobRunner)
        {
            _jobRunner = jobRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _jobRunner.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);

            while (!stoppingToken.IsCancellationRequested)
                await _jobRunner.TickAsync(stoppingToken);

            await _jobRunner.StopAsync(stoppingToken);
        }
    }
}