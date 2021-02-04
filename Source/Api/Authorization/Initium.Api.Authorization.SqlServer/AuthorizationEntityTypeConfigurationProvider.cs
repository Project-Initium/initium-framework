using System;
using System.Linq;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Authorization.Queries.Entities;
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
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Single(p => typeof(IAuthorizedReadOnlyUser).IsAssignableFrom(p) && 
                             !p.IsInterface && !p.IsAbstract);
            
            var t = modelBuilder.Entity(type);
            t.HasMany(nameof(IAuthorizedReadOnlyUser.Roles))
                
               
                .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field)
                ;
            // var navigation = t.Metadata.FindNavigation(nameof(IAuthorizedReadOnlyUser.Roles));
            // navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Single(p => typeof(IAuthorizedReadOnlyUser).IsAssignableFrom(p) && 
                             !p.IsInterface && p.IsAbstract);
            
            t = modelBuilder.Entity(type);
            t.HasKey("Id");
            
            //modelBuilder.ApplyConfiguration(new AuthorizedUserEntityTypeConfiguration(this._schemaIdentifier));
            modelBuilder.ApplyConfiguration(new ReadOnlyRoleEntityTypeConfiguration(this._schemaIdentifier));
            modelBuilder.ApplyConfiguration(new ReadOnlyResourceEntityTypeConfiguration(this._schemaIdentifier));
            
            
            modelBuilder.ApplyConfiguration(new ReadOnlyRoleNavigationalEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReadOnlyResourceNavigationalEntityTypeConfiguration());
            
        }
    }
}