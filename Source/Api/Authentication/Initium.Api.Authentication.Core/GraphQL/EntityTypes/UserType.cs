using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.GraphQL;

namespace Initium.Api.Authentication.Core.GraphQL.EntityTypes
{
    public class UserType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            //base.Configure(descriptor);
            descriptor.Name("User");
            descriptor.Field("Id")
                .Type<NonNullType<GuidType>>();
            descriptor.Field("ExternalRef")
                .Type<NonNullType<StringType>>();
        }
    }
}