using System;
using Finbuckle.MultiTenant;
using Initium.Api.Core.Settings;
using Initium.Api.MultiTenant.DependencyInjection;
using Initium.Api.MultiTenant.Extensions;
using Initium.Api.MultiTenant.Infrastructure.Settings;
using Initium.Api.MultiTenant.Infrastructure.TenantIdentification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Initium.Api.MultiTenant
{
    public static class ServiceCollectionExtensions
    {
        public static MultiTenantBuilder AddMultiTenantForApi(this IServiceCollection services,
            IConfiguration configuration)
        {
            return IServiceCollectionExtensions.AddMultiTenant(services, configuration, collection => 
                collection.AddMultiTenant<TenantInfo>()
                    .WithHeaderStrategy("x-tenant-id")
                    .WithStore(ServiceLifetime.Singleton, sp =>
                    {
                        var serviceProvider = sp.CreateScope().ServiceProvider;
                        var multiTenantSettings = serviceProvider.GetRequiredService<IOptions<MultiTenantSettings>>();
                        var dataSettings = serviceProvider.GetRequiredService<IOptions<DataSettings>>();
                        return new CustomMultiTenantStore(dataSettings, multiTenantSettings);
                    }));
        }
    }
}