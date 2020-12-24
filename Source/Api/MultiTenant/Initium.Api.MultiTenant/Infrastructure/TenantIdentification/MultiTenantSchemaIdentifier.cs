using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Finbuckle.MultiTenant;
using Initium.Api.Core.Database;

namespace Initium.Api.MultiTenant.Infrastructure.TenantIdentification
{
    public class MultiTenantSchemaIdentifier : ISchemaIdentifier
    {
        private readonly ITenantInfo _tenantInfo;
        private readonly Finbuckle.MultiTenant.IMultiTenantStore<TenantInfo> _multiTenantStore;

        public MultiTenantSchemaIdentifier(ITenantInfo tenantInfo, IMultiTenantStore<TenantInfo> multiTenantStore)
        {
            this._tenantInfo = tenantInfo;
            this._multiTenantStore = multiTenantStore;
        }

        public string SelectedSchema => this._tenantInfo.Id;

        public async Task<List<string>> SchemasInUse(CancellationToken cancellationToken)
        {
            var allTenants = await this._multiTenantStore.GetAllAsync();
            var tenantInfos = allTenants.ToList();
            return tenantInfos.Any() ? tenantInfos.Select(x => x.Id).ToList() : new List<string>{ "dbo" };
        }

    }
}