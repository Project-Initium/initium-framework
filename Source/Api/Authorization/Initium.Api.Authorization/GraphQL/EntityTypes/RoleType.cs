using HotChocolate.Types;
using Initium.Api.Authorization.Queries.Entities;

namespace Initium.Api.Authorization.GraphQL.EntityTypes
{
    public class RoleType : ObjectType<ReadOnlyRole>
    {
        protected override void Configure(IObjectTypeDescriptor<ReadOnlyRole> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Name("Role");
        }
    }
}