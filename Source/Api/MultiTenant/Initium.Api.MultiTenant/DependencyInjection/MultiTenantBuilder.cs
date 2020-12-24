using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.MultiTenant.DependencyInjection
{
    public class MultiTenantBuilder
    {
        public IServiceCollection Services { get; }
        
        public MultiTenantBuilder(IServiceCollection services)
        {
            this.Services = services;
        }
    }
}