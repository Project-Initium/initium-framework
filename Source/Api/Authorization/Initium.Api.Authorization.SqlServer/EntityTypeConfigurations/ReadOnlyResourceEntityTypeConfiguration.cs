using System.Collections.Generic;
using Initium.Api.Authorization.Queries.Entities;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.Authorization.SqlServer.EntityTypeConfigurations
{
    public class ReadOnlyResourceEntityTypeConfiguration : IEntityTypeConfiguration<ReadOnlyResource>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;

        public ReadOnlyResourceEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<ReadOnlyResource> readOnlyResources)
        {
            readOnlyResources.ToTable("Resource", "dbo");
            readOnlyResources.HasNoKey();
            
            var navigation = readOnlyResources.Metadata.FindNavigation(nameof(ReadOnlyResource.Resources));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            readOnlyResources.HasMany(x => x.Resources)
                .WithOne(x => x.Resource)
                .HasForeignKey(x => x.ParentResourceId);
            
            navigation = readOnlyResources.Metadata.FindNavigation(nameof(ReadOnlyResource.Roles));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            readOnlyResources
                .HasMany(x => x.Roles)
                .WithMany(x => x.Resources)
                .UsingEntity<Dictionary<string, object>>(
                    $"[{this._schemaIdentifier.SelectedSchema}].[vwRoleResource]",
                    b => b.HasOne<ReadOnlyRole>().WithMany().HasForeignKey("RoleId"),
                    b => b.HasOne<ReadOnlyResource>().WithMany().HasForeignKey("ResourceId"));

        }
    }
}