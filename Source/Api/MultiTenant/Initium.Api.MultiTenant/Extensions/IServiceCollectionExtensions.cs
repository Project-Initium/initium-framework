using System;
using Finbuckle.MultiTenant;
using FluentValidation;
using Initium.Api.Core.Database;
using Initium.Api.MultiTenant.DependencyInjection;
using Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate;
using Initium.Api.MultiTenant.Infrastructure.Repositories;
using Initium.Api.MultiTenant.Infrastructure.Settings;
using Initium.Api.MultiTenant.Infrastructure.TenantIdentification;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NodaTime;

namespace Initium.Api.MultiTenant.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static MultiTenantBuilder AddMultiTenant(
            this IServiceCollection services,
            IConfiguration configuration,
            Func<IServiceCollection, FinbuckleMultiTenantBuilder<TenantInfo>> tenantBuilder)
        {
            services.Configure<MultiTenantSettings>(configuration.GetSection("MultiTenant"));
            services.TryAddSingleton<IClock>(SystemClock.Instance);
            services.AddScoped<ITenantRepository, TenantRepository>();
            
            services.AddScoped<ISchemaIdentifier, MultiTenantSchemaIdentifier>();
            
            services.AddValidatorsFromAssembly(typeof(MultiTenantBuilder).Assembly);
            services.AddMediatR(typeof(MultiTenantBuilder).Assembly);

            tenantBuilder.Invoke(services);

            return new MultiTenantBuilder(services);
        }
        
    }
}