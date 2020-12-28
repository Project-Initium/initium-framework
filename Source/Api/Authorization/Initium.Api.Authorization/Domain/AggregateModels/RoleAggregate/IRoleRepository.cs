﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;

namespace Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate
{
    public interface IRoleRepository : IRepository<IRole>
    {
        IRole Add(IRole role);

        Task<Maybe<IRole>> Find(Guid id, CancellationToken cancellationToken = default);

        void Update(IRole role);

        void Delete(IRole role);
    }
}