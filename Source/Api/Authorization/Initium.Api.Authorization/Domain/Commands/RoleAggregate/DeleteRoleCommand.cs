﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authorization.Domain.Commands.RoleAggregate
{
    public class DeleteRoleCommand : IRequest<ResultWithError<ErrorData>>
    {
        public DeleteRoleCommand(Guid roleId)
        {
            this.RoleId = roleId;
        }

        public Guid RoleId { get; }
    }
}