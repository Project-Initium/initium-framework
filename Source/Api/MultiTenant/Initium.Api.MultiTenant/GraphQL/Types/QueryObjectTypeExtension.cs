using HotChocolate.Types;
using Initium.Api.Core.Database;
using Initium.Api.Core.GraphQL;
using Initium.Api.MultiTenant.GraphQL.Types.EntityTypes;
using Initium.Api.MultiTenant.Queries.Entities;

namespace Initium.Api.MultiTenant.GraphQL.Types
{
    public class QueryObjectTypeExtension : ObjectTypeExtension//<Query>
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(QueryType.TypeName);
            descriptor
                .Field("tenants")
                .Type<TenantType>()
                //.UseDbContext<DataContext>()
                .UseOffsetPaging<TenantType>()
                .UseProjection()
                .UseFiltering()
                .UseSorting()
                .Resolver((ctx, ct) =>
                {
                    var context = ctx.Service<GenericDataContext>();
                    return context.Set<ReadOnlyTenant>();
                });
        }
    }
}