using HotChocolate.Types;

namespace Initium.Api.Authentication.Core.GraphQL
{
    public class QueryObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(QueryType.TypeName);
            descriptor
                .Field("users")
                .Resolver((context, token) =>
                {
                                        
                    // query external
                    
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