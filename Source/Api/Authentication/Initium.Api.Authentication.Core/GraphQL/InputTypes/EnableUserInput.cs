// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using FluentValidation;

namespace Initium.Api.Authentication.Core.GraphQL.InputTypes
{
    public class EnableUserInput
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<EnableUserInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
            }
        }
    }
}