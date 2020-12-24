﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;

namespace Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate
{
    public interface ITenantRepository : IRepository<ITenant>
    {
        ITenant Add(ITenant tenant);

        void Update(ITenant tenant);

        Task<Maybe<ITenant>> Find(Guid tenantId, CancellationToken cancellationToken = default);
    }
}