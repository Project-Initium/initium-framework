using System.Collections.Generic;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authentication.Core.GraphQL.QueryTypes;
using Initium.Api.Authentication.Core.Queries.Entities;
//using Initium.Api.Authentication.Core.GraphQL;
//using Initium.Api.Authentication.Core.GraphQL.EntityTypes;
using Initium.Api.Authorization.GraphQL;
using Initium.Api.Authorization.GraphQL.EntityTypes;
using Initium.Api.Authorization.Queries.Entities;
using Initium.Api.Core.Database;
using Microsoft.EntityFrameworkCore;
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

    public class UserTypeExtension : ObjectTypeExtension<User>
    {
        protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        {
            //descriptor.Name("User");
            descriptor.Field("Roles")
                .Name("roles")
                .Type<RoleType>()
                .UseOffsetPaging<RoleType>()
                .UseProjection()
                .UseFiltering()
                .UseSorting()
                .Resolver((ctx, ct) =>
                {
                    var p = ctx.Parent<ReadOnlyUser>();
                    var context = ctx.Service<GenericDataContext>();
                    EF.Property<List<ReadOnlyRole>>(p, "_roles");
                    return context.Set<ReadOnlyRole>();
                });
        }

        // protected override void Configure(IObjectTypeDescriptor<User> descriptor)
        // {
        //     descriptor.Field("roles").Type<ListType<RoleType>>();
        // }
    }
}