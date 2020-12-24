using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using Initium.Api.Core.Database;
using Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate;
using MaybeMonad;
using Microsoft.EntityFrameworkCore;

namespace Initium.Api.MultiTenant.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly GenericDataContext _context;

        public TenantRepository(GenericDataContext context)
        {
            this._context = context;
        }

        public IUnitOfWork UnitOfWork => null; // this._context;

        public ITenant Add(ITenant tenant)
        {
            var entity = tenant as Tenant;
            if (entity == null)
            {
                throw new ArgumentException(nameof(tenant));
            }
            
            return this._context.Set<Tenant>().Add(entity).Entity;
        }

        public void Update(ITenant tenant)
        {
            var entity = tenant as Tenant;
            if (entity == null)
            {
                throw new ArgumentException(nameof(tenant));
            }
            
            this._context.Set<Tenant>().Update(entity);
        }

        public async Task<Maybe<ITenant>> Find(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var tenant = await this._context.Set<Tenant>().SingleOrDefaultAsync(x => x.Id == tenantId, cancellationToken);
            await this.Refresh(tenant);
            return Maybe.From<ITenant>(tenant);
        }

        private async Task Refresh(ITenant tenant)
        {
            if (tenant != null)
            {
                await this._context.Entry(tenant).ReloadAsync();
            }
        }
    }
}