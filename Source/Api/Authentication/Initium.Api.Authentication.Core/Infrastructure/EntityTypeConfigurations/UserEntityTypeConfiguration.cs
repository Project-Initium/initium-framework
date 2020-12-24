using Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.Authentication.Core.Infrastructure.EntityTypeConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;
    
        public UserEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<User> users)
        {
            users.ToTable("User", this._schemaIdentifier.SelectedSchema);
            users.HasKey(tenant => tenant.Id);
            users.Ignore(tenant => tenant.DomainEvents);
            users.Ignore(tenant => tenant.IntegrationEvents);
            users.Property(tenant => tenant.Id).ValueGeneratedNever();
        }
    }
}