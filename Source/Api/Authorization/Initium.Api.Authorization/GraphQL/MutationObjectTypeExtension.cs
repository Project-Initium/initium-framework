using FairyBread;
using HotChocolate.Types;
using Initium.Api.Authorization.Domain.Commands.RoleAggregate;
using Initium.Api.Authorization.GraphQL.EntityTypes;
using Initium.Api.Authorization.GraphQL.InputTypes;
using Initium.Api.Authorization.Queries.Entities;
using Initium.Api.Core.Extensions;
using MediatR;

namespace Initium.Api.Authorization.GraphQL
{
    public class MutationObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("createRole")
                .Argument("input", x => x.Type(typeof(CreateRoleInput)).UseValidation())
                .Resolve(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<CreateRoleInput>("input");
                    await mediator.ThrowOnError(new CreateRoleCommand(input.RoleId, input.Name, input.Resources),
                        cancellationToken: token);
                    return new ReadOnlyRole();
                })
                .Type<RoleType>();
            
            descriptor.Field("updateRole")
                .Argument("input", x => x.Type(typeof(UpdateRoleInput)).UseValidation())
                .Resolve(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<UpdateRoleInput>("input");
                    await mediator.ThrowOnError(new UpdateRoleCommand(input.RoleId, input.Name, input.Resources),
                        cancellationToken: token);
                    return new ReadOnlyRole();
                })
                .Type<RoleType>();
        }
    }
}