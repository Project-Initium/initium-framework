﻿using System.Collections.Generic;
using DbUp;
using Initium.Api.Core.Database;
using Initium.Api.Core.Settings;
using Microsoft.Extensions.Options;

namespace Initium.Api.MultiTenant.SqlServer.Database
{
    public class MultiTenantDbMigrationEngine : IDbMigrationEngine
    {
        private readonly DataSettings _dataSettings;

        public MultiTenantDbMigrationEngine(IOptions<DataSettings> dataSettings)
        {
            this._dataSettings = dataSettings.Value;
        }

        public bool PerformMigration(IDictionary<string, string> variables)
        {
            var connectionString = this._dataSettings.PrimaryConnectionString;

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .JournalToSqlTable("dbo", "SchemaVersions")
                    .WithScriptsEmbeddedInAssembly(this.GetType().Assembly)
                    .LogToConsole()
                    .WithVariables(variables??new Dictionary<string, string>())
                    .Build();

            var result = upgrader.PerformUpgrade();
            return result.Successful;
        }
    }
}