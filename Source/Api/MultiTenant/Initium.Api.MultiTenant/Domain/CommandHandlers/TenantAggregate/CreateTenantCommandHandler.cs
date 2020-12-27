// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

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
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, ResultWithError<ErrorData>>
    {
        private readonly GenericDataContext _context;
        private readonly ILogger _logger;
        private readonly ITenantRepository _tenantRepository;

        public CreateTenantCommandHandler(
            ITenantRepository tenantRepository,
            ILogger<CreateTenantCommandHandler> logger,
            GenericDataContext context)
        {
            this._tenantRepository = tenantRepository;
            this._logger = logger;
            this._context = context;
        }

        public async Task<ResultWithError<ErrorData>> Handle(
            CreateTenantCommand request,
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

        private async Task<ResultWithError<ErrorData>> Process(
            CreateTenantCommand request,
            CancellationToken cancellationToken)
        {
            var isAvailable = await this._context.Set<ReadOnlyTenant>()
                .CountAsync(x => x.Name == request.Name, cancellationToken) == 0;
            if (!isAvailable)
            {
                this._logger.LogDebug("Failed presence check.");
                return ResultWithError.Fail(new ErrorData(MultiTenantErrorCodes.TenantAlreadyExists));
            }

            var tenant = new Tenant(request.TenantId, request.Identifier, request.Name, request.ConnectionString);
            this._tenantRepository.Add(tenant);

            return ResultWithError.Ok<ErrorData>();
        }
    }
}