using Initium.Api.Core.Database;
using Initium.Api.MultiTenant.SqlServer.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.MultiTenant.SqlServer
{
    public class MultiTenantEntityTypeConfigurationProvider : IEntityTypeConfigurationProvider
    {
        private readonly ISchemaIdentifier _schemaIdentifier;

        public MultiTenantEntityTypeConfigurationProvider(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void ApplyConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantEntityTypeConfiguration(this._schemaIdentifier));
            modelBuilder.ApplyConfiguration(new ReadOnlyTenantEntityTypeConfiguration(this._schemaIdentifier));
        }
    }
}