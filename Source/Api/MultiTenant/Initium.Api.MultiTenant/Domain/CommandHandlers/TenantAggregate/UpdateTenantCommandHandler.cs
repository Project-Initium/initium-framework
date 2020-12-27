// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Database;
using Initium.Api.Core.Domain;
using Initium.Api.MultiTenant.Constants;
using Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate;
using Initium.Api.MultiTenant.Domain.Commands.TenantAggregate;
using Initium.Api.MultiTenant.Queries.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultMonad;

namespace Initium.Api.MultiTenant.Domain.CommandHandlers.TenantAggregate
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, ResultWithError<ErrorData>>
    {
        private readonly GenericDataContext _context;
        private readonly ILogger _logger;
        private readonly ITenantRepository _tenantRepository;

        public UpdateTenantCommandHandler(ITenantRepository tenantRepository,
            ILogger<UpdateTenantCommandHandler> logger, GenericDataContext context)
        {
            this._tenantRepository = tenantRepository;
            this._logger = logger;
            this._context = context;
        }

        public async Task<ResultWithError<ErrorData>> Handle(UpdateTenantCommand request,
            CancellationToken cancellationToken)
        {
            var result = await this.Process(request, cancellationToken);
            var dbResult = await this._tenantRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (dbResult)
            {
                return result;
            }

            this._logger.LogDebug("Failed saving changes.");
            return ResultWithError.Fail(new ErrorData(
                ErrorCodes.SavingChanges, "Failed To Save Database"));
        }

        private async Task<ResultWithError<ErrorData>> Process(UpdateTenantCommand request,
            CancellationToken cancellationToken)
        {
            var tenantMaybe = await this._tenantRepository.Find(request.TenantId, cancellationToken);
            if (tenantMaybe.HasNoValue)
            {
                this._logger.LogDebug("Entity not found.");
                return ResultWithError.Fail(new ErrorData(MultiTenantErrorCodes.TenantNotFound));
            }

            var tenant = tenantMaybe.Value;

            if (!string.Equals(tenant.Identifier, request.Identifier, StringComparison.InvariantCultureIgnoreCase))
            {
                var isAvailable = await this._context.Set<ReadOnlyTenant>()
                    .CountAsync(x => x.Name == request.Name, cancellationToken) == 0;
                if (!isAvailable)
                {
                    this._logger.LogDebug("Failed presence check.");
                    return ResultWithError.Fail(new ErrorData(MultiTenantErrorCodes.TenantAlreadyExists));
                }
            }

            tenant.UpdateDetails(request.Identifier, tenant.Name);

            this._tenantRepository.Update(tenant);
            return ResultWithError.Ok<ErrorData>();
        }
    }
}