using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions
{
    public interface IWorkerConfigurator
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}