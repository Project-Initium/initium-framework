using System.Collections.Generic;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Authorization.Queries.Entities;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.Authorization.SqlServer.EntityTypeConfigurations
{
    public class ReadOnlyRoleEntityTypeConfiguration : IEntityTypeConfiguration<ReadOnlyRole>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;

        public ReadOnlyRoleEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<ReadOnlyRole> readOnlyRoles)
        {
            readOnlyRoles.ToView("vwRole", this._schemaIdentifier.SelectedSchema);
            
            readOnlyRoles
                .HasMany(x => x.Users)
                .WithMany("Roles")
                .UsingEntity<Dictionary<string, object>>(
                    $"[{this._schemaIdentifier.SelectedSchema}].[vwUserRole]",
                    b => b.HasOne<ReadOnlyUser>().WithMany().HasForeignKey("UserId"),
                    b => b.HasOne<ReadOnlyRole>().WithMany().HasForeignKey("RoleId")
                ).Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
    
    public class ReadOnlyRoleNavigationalEntityTypeConfiguration : IEntityTypeConfiguration<ReadOnlyRole>
    {
        public void Configure(EntityTypeBuilder<ReadOnlyRole> readOnlyRoles)
        {
            // var navigation = readOnlyRoles.Metadata.FindNavigation(nameof(ReadOnlyRole.Resources));
            // navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}