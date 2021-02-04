using HotChocolate.Execution.Configuration;
using Initium.Api.Authorization.GraphQL;
using Initium.Api.Authorization.GraphQL.EntityTypes;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authorization.Extensions
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder RegisterAuthorization(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<RoleType>()
                .AddType<AuthorizedReadOnlyUserInterfaceType>()
                //.AddType<QueryObjectTypeExtension>()
                //.AddType<MutationObjectTypeExtension>()
                ;
        }
    }
}