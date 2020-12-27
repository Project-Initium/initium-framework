using Initium.Api.Authentication.Core.DependencyInjection;
using Initium.Api.Authentication.Core.Infrastructure;
using Initium.Api.Authentication.Core.SqlServer.Database;
using Initium.Api.Core.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authentication.Core.SqlServer
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder WithSqlServerStore(this AuthenticationBuilder builder)
        {
            builder.Services.AddScoped<IEntityTypeConfigurationProvider, AuthenticationEntityTypeConfigurationProvider>();
            builder.Services.AddScoped<IDbMigrationEngine, AuthenticationDbMigrationEngine>();
            return builder;
        }
    }
}