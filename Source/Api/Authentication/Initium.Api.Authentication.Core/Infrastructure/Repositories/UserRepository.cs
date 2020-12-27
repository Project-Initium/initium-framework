// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate;
using Initium.Api.Core.Contracts.Domain;
using Initium.Api.Core.Database;
using MaybeMonad;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.Authentication.Core.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GenericDataContext _context;

        public UserRepository(GenericDataContext context)
        {
            this._context = context;
        }

        public IUnitOfWork UnitOfWork => this._context;

        public IUser Add(IUser user)
        {
            var entity = user as User;
            if (entity == null)
            {
                throw new ArgumentException(nameof(user));
            }

            return this._context.Set<User>().Add(entity).Entity;
        }

        public void Update(IUser user)
        {
            var entity = user as User;
            if (entity == null)
            {
                throw new ArgumentException(nameof(user));
            }

            this._context.Set<User>().Update(entity);
        }

        public async Task<Maybe<IUser>> FindByExternalUserRef(string externalUserRef, CancellationToken cancellationToken = default)
        {
            var user = await this._context.Set<User>().SingleOrDefaultAsync(x => x.ExternalUserRef == externalUserRef, cancellationToken);
            await this.Refresh(user);
            return Maybe.From<IUser>(user);
        }

        public async Task<Maybe<IUser>> Find(Guid userId, CancellationToken cancellationToken = default)
        {
            var user = await this._context.Set<User>().SingleOrDefaultAsync(x => x.Id == userId, cancellationToken);
            await this.Refresh(user);
            return Maybe.From<IUser>(user);
        }

        private async Task Refresh(IUser user)
        {
            if (user != null)
            {
                await this._context.Entry(user).ReloadAsync();
            }
        }
    }
}