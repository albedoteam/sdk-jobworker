using System.Threading;
using System.Threading.Tasks;
using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
using Microsoft.Extensions.Logging;

namespace AlbedoTeam.Sdk.JobWorker
{
    public class ShrugJobRunner : IJobRunner
    {
        private readonly ILogger<ShrugJobRunner> _logger;

        public ShrugJobRunner(ILogger<ShrugJobRunner> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker is starting...");
            await Task.Delay(0, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("...Worker is stopped");
            await Task.Delay(0, cancellationToken);
        }

        public async Task TickAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker is running, but it does nothing ¯\\_(ツ)_/¯");
            await Task.Delay(10000, cancellationToken);
        }
    }
}