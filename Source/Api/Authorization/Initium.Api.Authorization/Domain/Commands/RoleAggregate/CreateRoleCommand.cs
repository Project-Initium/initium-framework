// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Initium.Api.Core.Domain;
using Initium.Portal.Domain.CommandResults.RoleAggregate;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authorization.Domain.Commands.RoleAggregate
{
    public class CreateRoleCommand : IRequest<Result<CreateRoleCommandResult, ErrorData>>
    {
        public CreateRoleCommand(Guid roleId, string name, List<Guid> resources)
        {
            this.Name = name;
            this.Resources = resources;
            this.RoleId = roleId;
        }

        public Guid RoleId { get; }

        public string Name { get; }

        public IReadOnlyList<Guid> Resources { get; }
    }
}