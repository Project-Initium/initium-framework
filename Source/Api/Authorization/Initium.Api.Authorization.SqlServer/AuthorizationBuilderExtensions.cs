using Initium.Api.Authorization.DependencyInjection;
using Initium.Api.Authorization.SqlServer.Database;
using Initium.Api.Core.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authorization.SqlServer
{
    public static class AuthorizationBuilderExtensions
    {
        public static AuthorizationBuilder WithSqlServerStore(this AuthorizationBuilder builder)
        {
            builder.Services.AddScoped<IEntityTypeConfigurationProvider, AuthorizationEntityTypeConfigurationProvider>();
            builder.Services.AddScoped<IDbMigrationEngine, AuthorizationDbMigrationEngine>();
            return builder;
        }
    }
}