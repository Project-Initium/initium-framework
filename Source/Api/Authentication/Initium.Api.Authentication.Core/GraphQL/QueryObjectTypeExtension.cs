using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;
using Initium.Api.Authentication.Core.Infrastructure;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.Database;
using Initium.Api.Core.GraphQL;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.Authentication.Core.GraphQL
{
    public class QueryObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(QueryType.TypeName);
            descriptor
                .Field("users")
                .Type<UserType>()
                .UseOffsetPaging<UserType>()
                .UseProjection()
                .UseFiltering()
                .UseSorting()
                .Resolver((ctx, token) =>
                {
                    var context = ctx.Service<GenericDataContext>();
                    
                        
                    var type = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Single(p => typeof(IAuthenticatedReadOnlyUser).IsAssignableFrom(p));

                    var contextType = typeof(GenericDataContext);
                    
                    context.Find(type);
                    return contextType.GetMethod("set").MakeGenericMethod(type).Invoke(context, null);
                    
                })
                ;
        }
        
    }
}