// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authorization.Constants;
using Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate;
using Initium.Api.Authorization.Domain.Commands.RoleAggregate;
using Initium.Api.Core.Database;
using Initium.Api.Core.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultMonad;

namespace Initium.Api.Authorization.Domain.CommandHandlers.RoleAggregate
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ResultWithError<ErrorData>>
    {
        private readonly ILogger<UpdateRoleCommandHandler> _logger;
        private readonly GenericDataContext _genericDataContext;
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, GenericDataContext genericDataContext,
            ILogger<UpdateRoleCommandHandler> logger)
        {
            this._roleRepository = roleRepository;
            this._genericDataContext = genericDataContext;
            this._logger = logger;
        }

        public async Task<ResultWithError<ErrorData>> Handle(
            UpdateRoleCommand request, CancellationToken cancellationToken)
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
            UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var roleMaybe = await this._roleRepository.Find(request.RoleId, cancellationToken);
            if (roleMaybe.HasNoValue)
            {
                this._logger.LogDebug("Entity not found.");
                return ResultWithError.Fail(new ErrorData(AuthorizationErrorCodes.RoleNotFound));
            }

            var role = roleMaybe.Value;

            if (!string.Equals(role.Name, request.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                var presenceResult = await this._genericDataContext.Set<Role>().AnyAsync(x =>x.Name == request.Name, cancellationToken);
                if (presenceResult)
                {
                    this._logger.LogDebug("Failed presence check.");
                    return ResultWithError.Fail(new ErrorData(AuthorizationErrorCodes.RoleAlreadyExists));
                }
            }

            role.UpdateName(request.Name);
            role.SetResources(request.Resources);

            this._roleRepository.Update(role);

            return ResultWithError.Ok<ErrorData>();
        }
    }
}