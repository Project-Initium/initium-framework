using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate;
using Initium.Api.Core.Contracts.Domain;
using Initium.Api.Core.Database;
using MaybeMonad;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.Authorization.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly GenericDataContext _context;

        public RoleRepository(GenericDataContext context)
        {
            this._context = context;
        }

        public IUnitOfWork UnitOfWork => this._context;

        public IRole Add(IRole role)
        {
            var entity = role as Role;
            if (entity == null)
            {
                throw new ArgumentException(nameof(role));
            }

            return this._context.Set<Role>().Add(entity).Entity;
        }

        public async Task<Maybe<IRole>> Find(Guid id, CancellationToken cancellationToken = default)
        {
            var role = await this._context.Set<Role>()
                .Include(x => x.RoleResources)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            await this.Refresh(role);

            return Maybe.From<IRole>(role);
        }

        public void Update(IRole role)
        {
            var entity = role as Role;
            if (entity == null)
            {
                throw new ArgumentException(nameof(role));
            }

            this._context.Set<Role>().Update(entity);
        }

        public void Delete(IRole role)
        {
            var entity = role as Role;
            if (entity == null)
            {
                throw new ArgumentException(nameof(role));
            }

            this._context.Set<Role>().Remove(entity);
        }
        
        private async Task Refresh(IRole role)
        {
            if (role != null)
            {
                await this._context.Entry(role).ReloadAsync();
            }
        }
    }
}