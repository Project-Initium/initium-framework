using Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate;
using Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate;
using Initium.Api.Authorization.Domain.AggregateModels.UserWithRoleAggregate;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.Authorization.SqlServer.EntityTypeConfigurations
{
    public class AuthorizedUserEntityTypeConfiguration : IEntityTypeConfiguration<AuthorizedAuthorizedUser>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;
    
        public AuthorizedUserEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<AuthorizedAuthorizedUser> userWithRoles)
        {
            userWithRoles.ToTable("User", this._schemaIdentifier.SelectedSchema);
            userWithRoles.HasKey(userWithRole => userWithRole.Id);
            userWithRoles.Ignore(userWithRole => userWithRole.DomainEvents);
            userWithRoles.Ignore(userWithRole => userWithRole.IntegrationEvents);
            userWithRoles.Property(userWithRole => userWithRole.Id).ValueGeneratedNever();
            
        }
    }
}