using HotChocolate.Types;
using Initium.Api.Authentication.Core.Infrastructure;
using Initium.Api.Core.Database;
using Initium.Api.Core.GraphQL;

namespace Initium.Api.Authentication.Core.GraphQL
{
    public class QueryObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(QueryType.TypeName);
            descriptor
                .Field("users")
                .Resolver((ctx, token) =>
                {
                    var context = ctx.Service<GenericDataContext>();
                    var identityProviderClient = ctx.Service<IIdentityProviderClient>();
                    
                    ctx.
                    // query external
                    identityProviderClient.Search(ctx.GetSelections())
                    
                    // get for database
                })
        }
        
    }

    public class UserType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("User");
            descriptor.Field("Id").Type()
        }
    }
}