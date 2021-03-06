﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;
using Initium.Api.Authorization.Domain.Commands.RoleAggregate;
using Initium.Api.Core.Contracts.Domain;

namespace Initium.Api.Authorization.Domain.CommandValidators.RoleAggregate
{
    public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            this.RuleFor(x => x.RoleId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEqual(Guid.Empty).WithErrorCode(ValidationCodes.FieldIsRequired);
        }
    }
}