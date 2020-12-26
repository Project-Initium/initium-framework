// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using FluentValidation;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;

namespace Initium.Api.Authentication.Core.Domain.CommandValidators.UserAggregate
{
    public class CreateInitialUserCommandValidator : AbstractValidator<CreateInitialUserCommand>
    {
        public CreateInitialUserCommandValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty();
            this.RuleFor(x => x.Password)
                .NotEmpty();
            this.RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress();
        }
    }
}