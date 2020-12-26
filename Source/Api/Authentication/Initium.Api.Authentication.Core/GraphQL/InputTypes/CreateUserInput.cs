// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;

namespace Initium.Api.Authentication.Core.GraphQL.InputTypes
{
    public class CreateUserInput
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public class Validator : AbstractValidator<CreateUserInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
                this.RuleFor(x => x.EmailAddress)
                    .NotEmpty();
                this.RuleFor(x => x.FirstName)
                    .NotEmpty();
                this.RuleFor(x => x.LastName)
                    .NotEmpty();
            }
        }
    }
}