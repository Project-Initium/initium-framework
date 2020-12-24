using FairyBread;
using HotChocolate.Types;
using Initium.Api.Core.Extensions;
using Initium.Api.Core.GraphQL;
using Initium.Api.MultiTenant.Domain.Commands.TenantAggregate;
using Initium.Api.MultiTenant.GraphQL.Inputs;
using Initium.Api.MultiTenant.GraphQL.Types.EntityTypes;
using Initium.Api.MultiTenant.Queries.Entities;
using MediatR;

namespace Initium.Api.MultiTenant.GraphQL.Types
{
    public class MutationObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(MutationType.TypeName);
            
            descriptor.Field("createTenant")
                .Argument("input", x=> x.Type(typeof(CreateTenantInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<CreateTenantInput>("input");
                    var result = await mediator.ThrowOnError(new CreateTenantCommand(input.Id, input.Identifier, input.Name, ""), token);
                    return new ReadOnlyTenant(input.Id, input.Identifier, input.Name, null);
                }).Type<NonNullType<TenantType>>();
            
            descriptor.Field("updateTenant")
                .Argument("input", x=> x.Type(typeof(UpdateTenantInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<UpdateTenantInput>("input");
                    var result = await mediator.ThrowOnError(new UpdateTenantCommand(input.Id, input.Identifier, input.Name), token);
                    return new ReadOnlyTenant(input.Id, input.Identifier, input.Name, null);
                }).Type<NonNullType<TenantType>>();
            
            descriptor.Field("disableTenant")
                .Argument("input", x=> x.Type(typeof(DisableTenantInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<DisableTenantInput>("input");
                    var result = await mediator.ThrowOnError(new DisableTenantCommand(input.Id), token);
                    return true;
                }).Type<NonNullType<BooleanType>>();
            
            descriptor.Field("enableTenant")
                .Argument("input", x=> x.Type(typeof(EnableTenantInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<EnableTenantInput>("input");
                    var result = await mediator.ThrowOnError(new EnableTenantCommand(input.Id), token);
                    return true;
                }).Type<NonNullType<BooleanType>>();
        }
    }
}