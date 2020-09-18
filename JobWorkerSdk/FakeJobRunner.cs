using System.Threading;
using System.Threading.Tasks;
using JobWorkerSdk.Configuration;
using Microsoft.Extensions.Logging;

namespace JobWorkerSdk
{
    public class FakeJobRunner : IJobRunner
    {
        private readonly ILogger<FakeJobRunner> _logger;

        public FakeJobRunner(ILogger<FakeJobRunner> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult<>(_logger.LogInformation("Worker is starting..."));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.FromResult<>(_logger.LogInformation("...Worker is stopped"));
        }
    }
}