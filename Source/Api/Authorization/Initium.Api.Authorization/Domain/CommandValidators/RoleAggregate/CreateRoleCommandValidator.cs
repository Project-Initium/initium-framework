// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using FluentValidation;
using Initium.Api.Authorization.Domain.Commands.RoleAggregate;
using Initium.Api.Core.Contracts.Domain;

namespace Initium.Api.Authorization.Domain.CommandValidators.RoleAggregate
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            this.RuleFor(x => x.RoleId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationCodes.FieldIsRequired);
            
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithErrorCode(ValidationCodes.FieldIsRequired);
        }
    }
}