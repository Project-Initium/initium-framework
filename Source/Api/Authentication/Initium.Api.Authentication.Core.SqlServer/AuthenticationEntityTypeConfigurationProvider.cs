using Initium.Api.Authentication.Core.SqlServer.EntityTypeConfigurations;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.Authentication.Core.SqlServer
{
    public class AuthenticationEntityTypeConfigurationProvider : IEntityTypeConfigurationProvider
    {
        private readonly ISchemaIdentifier _schemaIdentifier;

        public AuthenticationEntityTypeConfigurationProvider(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration(this._schemaIdentifier));
            modelBuilder.ApplyConfiguration(new ReadOnlyUserEntityTypeConfiguration(this._schemaIdentifier));
        }
    }
}