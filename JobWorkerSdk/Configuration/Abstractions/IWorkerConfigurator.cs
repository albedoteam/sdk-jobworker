using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobWorkerSdk.Configuration.Abstractions
{
    public interface IWorkerConfigurator
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}