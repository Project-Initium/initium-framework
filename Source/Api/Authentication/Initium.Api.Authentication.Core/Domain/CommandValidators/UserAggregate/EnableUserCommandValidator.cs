﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using FluentValidation;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;

namespace Initium.Api.Authentication.Core.Domain.CommandValidators.UserAggregate
{
    public class EnableUserCommandValidator : AbstractValidator<EnableUserCommand>
    {
        public EnableUserCommandValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}