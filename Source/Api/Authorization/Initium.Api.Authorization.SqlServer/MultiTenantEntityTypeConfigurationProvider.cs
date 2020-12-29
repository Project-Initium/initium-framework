using Initium.Api.Authorization.SqlServer.EntityTypeConfigurations;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.Authorization.SqlServer
{
    public class AuthorizationEntityTypeConfigurationProvider : IEntityTypeConfigurationProvider
    {
        private readonly ISchemaIdentifier _schemaIdentifier;

        public AuthorizationEntityTypeConfigurationProvider(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorizedUserEntityTypeConfiguration(this._schemaIdentifier));
            modelBuilder.ApplyConfiguration(new ReadOnlyRoleEntityTypeConfiguration(this._schemaIdentifier));
            modelBuilder.ApplyConfiguration(new ReadOnlyResourceEntityTypeConfiguration(this._schemaIdentifier));
            
        }
    }
}