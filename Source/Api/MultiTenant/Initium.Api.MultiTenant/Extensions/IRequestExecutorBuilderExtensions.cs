using HotChocolate.Execution.Configuration;
using Initium.Api.MultiTenant.GraphQL.Types;
using Initium.Api.MultiTenant.GraphQL.Types.EntityTypes;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.MultiTenant.Extensions
{
    public static class IRequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder RegisterMultiTenant(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<TenantType>()
                .AddType<QueryObjectTypeExtension>()
                .AddType<MutationObjectTypeExtension>();
        }
    }
}