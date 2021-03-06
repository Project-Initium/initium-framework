﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using FairyBread;
using HotChocolate.Types;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;
using Initium.Api.Authentication.Core.GraphQL.InputTypes;
using Initium.Api.Core.Extensions;
using Initium.Api.Core.GraphQL;
using MediatR;

namespace Initium.Api.Authentication.Core.GraphQL
{
    public class MutationObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(MutationType.TypeName);

            descriptor.Field("createUser")
                .Argument("input", x => x.Type(typeof(CreateUserInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<CreateUserInput>("input");
                    var result = await mediator.ThrowOnError(new CreateUserCommand(input.Id, input.EmailAddress, input.FirstName, input.LastName), token);
                    return result.IsSuccess;
                }).Type<NonNullType<BooleanType>>();

            descriptor.Field("createInitialUser")
                .Argument("input", x => x.Type(typeof(CreateInitialUserInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<CreateInitialUserInput>("input");
                    var result = await mediator.ThrowOnError(new CreateInitialUserCommand(input.Id, input.EmailAddress, input.FirstName, input.LastName, input.Password), token);
                    return result.IsSuccess;
                }).Type<NonNullType<BooleanType>>();

            descriptor.Field("changePassword")
                .Argument("input", x => x.Type(typeof(ChangePasswordInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<ChangePasswordInput>("input");
                    var result = await mediator.ThrowOnError(new ChangePasswordCommand(input.Id, input.NewPassword), token);
                    return result.IsSuccess;
                }).Type<NonNullType<BooleanType>>();

            descriptor.Field("disableUser")
                .Argument("input", x => x.Type(typeof(DisableUserInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<DisableUserInput>("input");
                    var result = await mediator.ThrowOnError(new DisableUserCommand(input.Id), token);
                    return result.IsSuccess;
                }).Type<NonNullType<BooleanType>>();

            descriptor.Field("enableUser")
                .Argument("input", x => x.Type(typeof(EnableUserInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<EnableUserInput>("input");
                    var result = await mediator.ThrowOnError(new EnableUserCommand(input.Id), token);
                    return result.IsSuccess;
                }).Type<NonNullType<BooleanType>>();
        }
    }
}