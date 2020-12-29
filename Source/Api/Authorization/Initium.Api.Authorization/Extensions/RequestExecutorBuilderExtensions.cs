using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;
//using Initium.Api.Authentication.Core.GraphQL;
//using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authorization.GraphQL;
using Initium.Api.Authorization.GraphQL.EntityTypes;
using Initium.Api.Authorization.Queries.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authorization.Extensions
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder RegisterAuthorization(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<RoleType>()
                .AddType<UserTypeExtension>()
                //.AddType<QueryObjectTypeExtension>()
                //.AddType<MutationObjectTypeExtension>()
                ;
        }
    }

    public class UserTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("users");
            descriptor.Field("roles").Type<ListType<RoleType>>();
        }

        // protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        // {
        //     descriptor.Field("roles").Type<ListType<RoleType>>();
        // }
    }
}