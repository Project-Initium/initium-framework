// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using HotChocolate.Types;
using Initium.Api.Core.Database;
using Initium.Api.Core.GraphQL;
using Initium.Examples.Api.Infrastructure.Entities;
using Initium.Examples.Api.Infrastructure.GraphQL.Types;

namespace Initium.Examples.Api.Infrastructure.GraphQL
{
    public class CustomQueryTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(CustomQueryType.TypeName);
            descriptor.Field("users")
                .Type<UserType>()
                .UseOffsetPaging<UserType>()
                .UseProjection()
                .UseFiltering()
                .UseSorting()
                .Resolver((ctx, ct) =>
                {
                    var context = ctx.Service<GenericDataContext>();
                    return context.Set<ReadOnlyUser>();
                });
        }
    }
}