using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.GraphQL;

namespace Initium.Api.Authentication.Core.GraphQL.EntityTypes
{
    public class UserType : ObjectType
    {
        public override TypeKind Kind => TypeKind.Object;

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            var type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Single(x => typeof(IReadOnlyUser).IsAssignableFrom(x) && !x.IsInterface);
            //descriptor.p
            descriptor.Name("User");
            // // descriptor.Field("Id")
            // //     .Name("id")
            // //     .Type<NonNullType<GuidType>>()
            // //     .ResolveWith(type.GetProperty("Id"));
            // // descriptor.IsOfType((context, result) => 
            // //     result.GetType() == type);
            descriptor.Field("ExternalRef")
                .Name("externalRef")
                .Type<StringType>()
                .UseProjection()
                .ResolveWith(type.GetProperty("ExternalRef"));
        }

    }
}