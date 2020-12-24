using HotChocolate.Types;
using Initium.Api.MultiTenant.Queries.Entities;

namespace Initium.Api.MultiTenant.GraphQL.Types.EntityTypes
{
    public class TenantType : ObjectType<ReadOnlyTenant>
    {
        protected override void Configure(IObjectTypeDescriptor<ReadOnlyTenant> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Name("Tenant");
        }
    }
}