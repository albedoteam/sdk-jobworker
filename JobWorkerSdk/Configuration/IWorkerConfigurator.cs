using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobWorkerSdk.Configuration
{
    public interface IWorkerConfigurator
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}