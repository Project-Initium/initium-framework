// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authorization.Constants;
using Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate;
using Initium.Api.Authorization.Domain.Commands.RoleAggregate;
using Initium.Api.Core.Database;
using Initium.Api.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using ResultMonad;

namespace Initium.Api.Authorization.Domain.CommandHandlers.RoleAggregate
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ResultWithError<ErrorData>>
    {
        private readonly GenericDataContext _genericDataContext;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<DeleteRoleCommandHandler> _logger;

        public DeleteRoleCommandHandler(IRoleRepository roleRepository, GenericDataContext genericDataContext, ILogger<DeleteRoleCommandHandler> logger)
        {
            this._roleRepository = roleRepository;
            this._genericDataContext = genericDataContext;
            this._logger = logger;
        }

        public async Task<ResultWithError<ErrorData>> Handle(
            DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await this.Process(request, cancellationToken);
            var dbResult = await this._roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (dbResult)
            {
                return result;
            }

            this._logger.LogDebug("Failed saving changes.");
            return ResultWithError.Fail(new ErrorData(
                ErrorCodes.SavingChanges, "Failed To Save Database"));
        }

        private async Task<ResultWithError<ErrorData>> Process(
            DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleMaybe = await this._roleRepository.Find(request.RoleId, cancellationToken);
            if (roleMaybe.HasNoValue)
            {
                this._logger.LogDebug("Entity not found.");
                return ResultWithError.Fail(new ErrorData(AuthorizationErrorCodes.RoleNotFound));
            }

            // var presenceResult = await this._genericDataContext.Set<R>().CheckForRoleUsageById(request.RoleId);
            // if (presenceResult.IsPresent)
            // {
            //     this._logger.LogDebug("Failed presence check.");
            //     return ResultWithError.Fail(new ErrorData(AuthorizationErrorCodes.RoleInUse));
            // }

            var role = roleMaybe.Value;

            this._roleRepository.Delete(role);

            return ResultWithError.Ok<ErrorData>();
        }
    }
}