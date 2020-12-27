// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using Finbuckle.MultiTenant;
using FluentValidation;
using Initium.Api.Core.Database;
using Initium.Api.Core.Settings;
using Initium.Api.MultiTenant.DependencyInjection;
using Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate;
using Initium.Api.MultiTenant.Infrastructure.Repositories;
using Initium.Api.MultiTenant.Infrastructure.Settings;
using Initium.Api.MultiTenant.Infrastructure.TenantIdentification;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NodaTime;

namespace Initium.Api.MultiTenant.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static MultiTenantBuilder AddInitiumMultiTenant(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MultiTenantSettings>(configuration.GetSection("MultiTenant"));
            services.TryAddSingleton<IClock>(SystemClock.Instance);
            services.AddScoped<ITenantRepository, TenantRepository>();

            services.AddScoped<ISchemaIdentifier, MultiTenantSchemaIdentifier>();

            services.AddValidatorsFromAssembly(typeof(MultiTenantBuilder).Assembly);
            services.AddMediatR(typeof(MultiTenantBuilder).Assembly);

            services.AddMultiTenant<TenantInfo>()
                .WithHeaderStrategy("x-tenant-id")
                .WithStore(ServiceLifetime.Singleton, sp =>
                {
                    var serviceProvider = sp.CreateScope().ServiceProvider;
                    var multiTenantSettings = serviceProvider.GetRequiredService<IOptions<MultiTenantSettings>>();
                    var dataSettings = serviceProvider.GetRequiredService<IOptions<DataSettings>>();
                    return new CustomMultiTenantStore(dataSettings, multiTenantSettings);
                });

            return new MultiTenantBuilder(services);
        }
    }
}