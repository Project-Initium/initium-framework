﻿// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Initium.Examples.Api
{
    public class StartupService : IHostedService
    {
        private readonly IServiceProvider _services;

        public StartupService(IServiceProvider services)
        {
            this._services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceProvider = this._services.CreateScope().ServiceProvider;

            var dbMigrationEngines = serviceProvider.GetServices<IDbMigrationEngine>();
            var migrationEngines = dbMigrationEngines.ToList();
            if (!migrationEngines.Any())
            {
                return;
            }

            var schemaIdentifier = serviceProvider.GetRequiredService<ISchemaIdentifier>();

            foreach (var schema in await schemaIdentifier.SchemasInUse(cancellationToken))
            {
                foreach (var dbMigrationEngine in migrationEngines)
                {
                    dbMigrationEngine.PerformMigration(new Dictionary<string, string>
                    {
                        { "schema", schema },
                    });
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}