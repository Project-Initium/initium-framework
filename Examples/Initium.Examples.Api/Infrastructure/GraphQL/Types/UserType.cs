using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authorization.GraphQL.EntityTypes;
using Initium.Examples.Api.Infrastructure.Entities;

namespace Initium.Examples.Api.Infrastructure.GraphQL.Types
{
    public class UserType : ObjectType<ReadOnlyUser>
    {
        protected override void Configure(IObjectTypeDescriptor<ReadOnlyUser> descriptor)
        {
            descriptor.Implements<AuthenticatedReadOnlyUserInterfaceType>();
            descriptor.Implements<AuthorizedReadOnlyUserInterfaceType>();
            descriptor.Name("User");
        }
    }
}