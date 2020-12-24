using Initium.Api.Core.Database;
using Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.MultiTenant.SqlServer.EntityTypeConfigurations
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;
        
        public TenantEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<Tenant> tenants)
        {
            tenants.ToTable("Tenant", this._schemaIdentifier.SelectedSchema);
            tenants.HasKey(tenant => tenant.Id);
            tenants.Ignore(tenant => tenant.DomainEvents);
            tenants.Ignore(tenant => tenant.IntegrationEvents);
            tenants.Property(tenant => tenant.Id).ValueGeneratedNever();
        }
    }
}