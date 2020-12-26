using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;

namespace Initium.Api.Authentication.Core.GraphQL.EntityTypes
{
    public class UserType : ObjectType<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            base.Configure(descriptor);
            descriptor.Name("User");
        }
    }
}