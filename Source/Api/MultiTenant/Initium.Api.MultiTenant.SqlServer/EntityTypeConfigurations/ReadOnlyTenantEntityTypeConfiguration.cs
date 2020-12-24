using Initium.Api.Core.Database;
using Initium.Api.MultiTenant.Queries.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.MultiTenant.SqlServer.EntityTypeConfigurations
{
    public class ReadOnlyTenantEntityTypeConfiguration : IEntityTypeConfiguration<ReadOnlyTenant>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;

        public ReadOnlyTenantEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<ReadOnlyTenant> readOnlyTenants)
        {
            
            readOnlyTenants.ToTable("vwTenant", this._schemaIdentifier.SelectedSchema);
            readOnlyTenants.HasNoKey();
        }
    }
}