// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;

namespace Initium.Api.Authentication.Core.GraphQL.InputTypes
{
    public class ChangePasswordInput
    {
        public Guid Id { get; set; }

        public string NewPassword { get; }

        public class Validator : AbstractValidator<ChangePasswordInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
                this.RuleFor(x => x.NewPassword)
                    .NotEmpty();
            }
        }
    }
}