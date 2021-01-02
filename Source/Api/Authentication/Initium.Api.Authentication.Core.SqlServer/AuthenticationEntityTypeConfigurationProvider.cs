using System;
using System.Linq;
using System.Reflection;
using Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            modelBuilder.Entity<User>(this.ConfigureUser);
            
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Single(p => typeof(IReadOnlyUser).IsAssignableFrom(p) && 
                             !p.IsInterface);

                var mbType = typeof(ModelBuilder);
            var method = mbType.GetMethod(nameof(ModelBuilder.Entity), 0);//, types: new []{typeof(Type)});
            

            var t = modelBuilder.Entity(type);
                
            t.ToTable("vwUser", this._schemaIdentifier.SelectedSchema);
            t.HasKey("Id");

        }

        private void ConfigureUser(EntityTypeBuilder<User> users)
        {
            users.ToTable("User", this._schemaIdentifier.SelectedSchema);
            users.HasKey(tenant => tenant.Id);
            users.Ignore(tenant => tenant.DomainEvents);
            users.Ignore(tenant => tenant.IntegrationEvents);
            users.Property(tenant => tenant.Id).ValueGeneratedNever();
        }
    }
}