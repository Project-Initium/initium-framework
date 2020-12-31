using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;
using Initium.Api.Authentication.Core.Queries.Entities;

namespace Initium.Api.Authentication.Core.GraphQL.EntityTypes
{
    public class UserType : ObjectType<IAuthenticatedReadOnlyUser>
    {
        protected override void Configure(IObjectTypeDescriptor<IAuthenticatedReadOnlyUser> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Name("User");
        }
    }
}