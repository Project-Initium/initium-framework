// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class CreateUserCommand : IRequest<ResultWithError<ErrorData>>
    {
        public CreateUserCommand(Guid userId, string emailAddress, string firstName, string lastName)
        {
            this.UserId = userId;
            this.EmailAddress = emailAddress;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Guid UserId { get; }

        public string EmailAddress { get; }

        public string FirstName { get; }

        public string LastName { get; }
    }
}