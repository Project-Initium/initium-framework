// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Domain;
using Initium.Api.MultiTenant.Constants;
using Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate;
using Initium.Api.MultiTenant.Domain.Commands.TenantAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using NodaTime;
using ResultMonad;

namespace Initium.Api.MultiTenant.Domain.CommandHandlers.TenantAggregate
{
    public class DisableTenantCommandHandler : IRequestHandler<DisableTenantCommand, ResultWithError<ErrorData>>
    {
        private readonly IClock _clock;
        private readonly ILogger _logger;
        private readonly ITenantRepository _tenantRepository;

        public DisableTenantCommandHandler(
            ITenantRepository tenantRepository,
            ILogger<DisableTenantCommandHandler> logger,
            IClock clock)
        {
            this._tenantRepository = tenantRepository;
            this._logger = logger;
            this._clock = clock;
        }

        public async Task<ResultWithError<ErrorData>> Handle(
            DisableTenantCommand request,
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
            DisableTenantCommand request,
            CancellationToken cancellationToken)
        {
            var tenantMaybe = await this._tenantRepository.Find(request.TenantId, cancellationToken);
            if (tenantMaybe.HasNoValue)
            {
                this._logger.LogDebug("Entity not found.");
                return ResultWithError.Fail(new ErrorData(MultiTenantErrorCodes.TenantNotFound));
            }

            var tenant = tenantMaybe.Value;

            tenant.Disable(this._clock.GetCurrentInstant().ToDateTimeUtc());

            this._tenantRepository.Update(tenant);
            return ResultWithError.Ok<ErrorData>();
        }
    }
}