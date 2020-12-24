using Initium.Api.Core.Database;
using Initium.Api.MultiTenant.DependencyInjection;
using Initium.Api.MultiTenant.SqlServer.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.MultiTenant.SqlServer
{
    public static class MultiTenantBuilderExtensions
    {
        public static MultiTenantBuilder WithSqlServerStore(this MultiTenantBuilder builder)
        {
            builder.Services.AddScoped<IEntityTypeConfigurationProvider, MultiTenantEntityTypeConfigurationProvider>();
            builder.Services.AddScoped<IDbMigrationEngine, MultiTenantDbMigrationEngine>();
            return builder;
        }
    }
}