using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Finbuckle.MultiTenant;
using Initium.Api.Core.Settings;
using Initium.Api.MultiTenant.Infrastructure.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Initium.Api.MultiTenant.Infrastructure.TenantIdentification
{
    public class CustomMultiTenantStore : IMultiTenantStore<TenantInfo>
    {
        private readonly DataSettings _dataSettings;
        private readonly MultiTenantSettings _multiTenantSettings;
        
        public CustomMultiTenantStore(IOptions<DataSettings> dataSettings, IOptions<MultiTenantSettings> multiTenantSettings)
        {
            this._multiTenantSettings = multiTenantSettings.Value;
            this._dataSettings = dataSettings.Value;
        }

        public Task<bool> TryAddAsync(TenantInfo tenantInfo)
        {
            return Task.FromResult(true);
        }

        public Task<bool> TryUpdateAsync(TenantInfo tenantInfo)
        {
            return Task.FromResult(true);
        }

        public Task<bool> TryRemoveAsync(string id)
        {
            return Task.FromResult(true);
        }

        public async Task<TenantInfo> TryGetByIdentifierAsync(string identifier)
        {
            if (string.Equals(identifier, this._multiTenantSettings.DefaultIdentifier,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return new TenantInfo
                {
                    Id = "dbo",
                    Identifier = this._multiTenantSettings.DefaultIdentifier,
                    Name = this._multiTenantSettings.DefaultName,
                    ConnectionString = this._dataSettings.PrimaryConnectionString,
                };
            }
            
            await using var connection = new SqlConnection(this._dataSettings.PrimaryConnectionString);
            await connection.OpenAsync();
            
            var tenant = await connection.QuerySingleOrDefaultAsync<TenantInfo>("[dbo].[uspGetTenantInfoByIdentifier]",
                new {identifier = identifier}, 
                commandType: CommandType.StoredProcedure);

            return tenant;
        }

        public async Task<TenantInfo> TryGetAsync(string id)
        {
            if (!Guid.TryParse(id, out var realId))
            {
                return null;
            }

            if (realId == this._multiTenantSettings.DefaultTenantId)
            {
                return new TenantInfo
                {
                    Id = "dbo",
                    Identifier = this._multiTenantSettings.DefaultIdentifier,
                    Name = this._multiTenantSettings.DefaultName,
                    ConnectionString = this._dataSettings.PrimaryConnectionString,
                };
            }

            await using var connection = new SqlConnection(this._dataSettings.PrimaryConnectionString);
            await connection.OpenAsync();
            
            var tenant = await connection.QuerySingleOrDefaultAsync<TenantInfo>("[dbo].[uspGetTenantInfoByIdentifier]",
                new {id = realId}, 
                commandType: CommandType.StoredProcedure);

            return tenant;
        }

        public async Task<IEnumerable<TenantInfo>> GetAllAsync()
        {
            try
            {
                await using var connection = new SqlConnection(this._dataSettings.PrimaryConnectionString);
                await connection.OpenAsync();
            
                var tenants = await connection.QueryAsync<TenantInfo>("[dbo].[uspGetAllTenantInfos]",
                    commandType: CommandType.StoredProcedure);

                return tenants;
            }
            catch (SqlException)
            {
                return new List<TenantInfo>
                {
                    new TenantInfo
                    {
                        Id = "dbo",
                        Identifier = this._multiTenantSettings.DefaultIdentifier,
                        Name = this._multiTenantSettings.DefaultName,
                        ConnectionString = this._dataSettings.PrimaryConnectionString,
                    }
                };
            }
            
        }
    }
}