﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.MultiTenant.Domain.Commands.TenantAggregate
{
    public class EnableTenantCommand : IRequest<ResultWithError<ErrorData>>
    {
        public EnableTenantCommand(Guid tenantId)
        {
            this.TenantId = tenantId;
        }

        public Guid TenantId { get; }
    }
}