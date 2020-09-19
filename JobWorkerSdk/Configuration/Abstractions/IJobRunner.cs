using System.Threading;
using System.Threading.Tasks;

namespace JobWorkerSdk.Configuration.Abstractions
{
    public interface IJobRunner
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        Task TickAsync(CancellationToken cancellationToken);
    }
}