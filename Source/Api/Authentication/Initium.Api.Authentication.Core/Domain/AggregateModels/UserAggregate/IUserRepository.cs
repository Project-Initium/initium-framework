// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;

namespace Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate
{
    public interface IUserRepository : IRepository<IUser>
    {
        IUser Add(IUser user);

        void Update(IUser user);

        Task<Maybe<IUser>> FindByExternalUserRef(string externalUserRef, CancellationToken cancellationToken = default);

        Task<Maybe<IUser>> Find(Guid userId, CancellationToken cancellationToken = default);
    }
}