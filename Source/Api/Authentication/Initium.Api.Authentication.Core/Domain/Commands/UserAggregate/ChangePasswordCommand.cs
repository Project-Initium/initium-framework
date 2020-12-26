// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class ChangePasswordCommand : IRequest<ResultWithError<ErrorData>>
    {
        public ChangePasswordCommand(Guid userId, string newPassword)
        {
            this.UserId = userId;
            this.NewPassword = newPassword;
        }

        public Guid UserId { get; }

        public string NewPassword { get; }
    }
}