// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using Initium.Api.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Core.Database
{
    public class GenericDataContext : DbContext, IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericDataContext(DbContextOptions<GenericDataContext> options, IServiceProvider serviceProvider)
            : base(options)
        {
            this._serviceProvider = serviceProvider;
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var mediator = this.GetService<IMediator>();
            await mediator.DispatchDomainEventsAsync(this);
            await this.SaveChangesAsync(cancellationToken);
            await mediator.DispatchIntegrationEventsAsync(this);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var providers = this._serviceProvider.GetServices<IEntityTypeConfigurationProvider>();
            foreach (var entityTypeConfigurationProvider in providers)
            {
                entityTypeConfigurationProvider.ApplyConfigurations(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}