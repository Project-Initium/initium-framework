// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using FluentValidation;
using Initium.Api.Authorization.Domain.Commands.RoleAggregate;
using Initium.Api.Core.Contracts.Domain;
using Initium.Portal.Core.Contracts.Domain;

namespace Initium.Portal.Domain.CommandValidators.RoleAggregate
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            this.RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithErrorCode(ValidationCodes.FieldIsRequired);
        }
    }
}