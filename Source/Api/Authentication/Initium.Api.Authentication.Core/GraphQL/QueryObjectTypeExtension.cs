using System;
using System.Linq;
using System.Reflection;
using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.Database;
using Initium.Api.Core.GraphQL;

namespace Initium.Api.Authentication.Core.GraphQL
{

    public class CustomQueryTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Single(p => typeof(IReadOnlyUser).IsAssignableFrom(p) && !p.IsInterface)
                ;
            
            descriptor.Name(CustomQueryType.TypeName);
            var users = descriptor
                .Field("users")
                .Type<UserType>()
                .Resolver((ctx, token) =>
                {
                    var context = ctx.Service<GenericDataContext>();

                    var contextType = typeof(GenericDataContext);
                    
                    var m = contextType.GetMethod(
                        nameof(GenericDataContext.Set), new Type[] {}
                    );
                    var gm = m.MakeGenericMethod(type);
                    return gm.Invoke(context, null);
                });
            
            var method = typeof(ProjectionObjectFieldDescriptorExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(x=>x.Name == nameof(ProjectionObjectFieldDescriptorExtensions.UseProjection) && x.GetGenericArguments().Length > 0)
                .MakeGenericMethod(new[] { type });
            method.Invoke(null, new object[] {users, null});
        }
    }
}