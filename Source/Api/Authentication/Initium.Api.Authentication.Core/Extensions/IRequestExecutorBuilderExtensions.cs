using HotChocolate.Execution.Configuration;
using Initium.Api.Authentication.Core.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authentication.Core.Extensions
{
    public static class IRequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder RegisterMultiTenant(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<QueryObjectTypeExtension>()
                .AddType<MutationObjectTypeExtension>();
        }
    }
}