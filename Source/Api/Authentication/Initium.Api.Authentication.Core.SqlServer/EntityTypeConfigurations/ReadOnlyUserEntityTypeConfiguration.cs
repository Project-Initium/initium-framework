﻿using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Initium.Api.Authentication.Core.SqlServer.EntityTypeConfigurations
{
    public class ReadOnlyUserEntityTypeConfiguration : IEntityTypeConfiguration<ReadOnlyUser>
    {
        private readonly ISchemaIdentifier _schemaIdentifier;
    
        public ReadOnlyUserEntityTypeConfiguration(ISchemaIdentifier schemaIdentifier)
        {
            this._schemaIdentifier = schemaIdentifier;
        }

        public void Configure(EntityTypeBuilder<ReadOnlyUser> users)
        {
            users.ToTable("vwUser", this._schemaIdentifier.SelectedSchema);
            users.HasNoKey();
        }
    }
}