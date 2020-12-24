// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;
using Initium.Api.Core.Contracts.Domain;
using Initium.Api.MultiTenant.Domain.Commands.TenantAggregate;

namespace Initium.Api.MultiTenant.Domain.CommandValidators.TenantAggregate
{
    public class DisableTenantCommandValidator : AbstractValidator<DisableTenantCommand>
    {
        public DisableTenantCommandValidator()
        {
            this.RuleFor(x => x.TenantId)
                .NotEqual(Guid.Empty).WithErrorCode(ValidationCodes.FieldIsRequired);
        }
    }
}