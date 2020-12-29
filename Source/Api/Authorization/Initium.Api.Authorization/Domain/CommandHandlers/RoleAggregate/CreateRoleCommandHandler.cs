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
using Initium.Portal.Domain.CommandResults.RoleAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResultMonad;

namespace Initium.Api.Authorization.Domain.CommandHandlers.RoleAggregate
{
    public class CreateRoleCommandHandler
        : IRequestHandler<CreateRoleCommand, Result<CreateRoleCommandResult, ErrorData>>
    {
        private readonly GenericDataContext _genericDataContext;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, GenericDataContext genericDataContext, ILogger<CreateRoleCommandHandler> logger)
        {
            this._roleRepository = roleRepository;
            this._genericDataContext = genericDataContext;
            this._logger = logger;
        }

        public async Task<Result<CreateRoleCommandResult, ErrorData>> Handle(
            CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await this.Process(request, cancellationToken);
            var dbResult = await this._roleRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (dbResult)
            {
                return result;
            }

            this._logger.LogDebug("Failed saving changes.");
            return Result.Fail<CreateRoleCommandResult, ErrorData>(new ErrorData(
                ErrorCodes.SavingChanges, "Failed To Save Database"));
        }

        private async Task<Result<CreateRoleCommandResult, ErrorData>> Process(
            CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var presenceResult = await this._genericDataContext.Set<Role>().AnyAsync(x =>x.Name == request.Name, cancellationToken);
            if (presenceResult)
            {
                this._logger.LogDebug("Failed presence check.");
                return Result.Fail<CreateRoleCommandResult, ErrorData>(new ErrorData(AuthorizationErrorCodes.RoleAlreadyExists));
            }

            var role = new Role(request.RoleId, request.Name, request.Resources);
            this._roleRepository.Add(role);

            return Result.Ok<CreateRoleCommandResult, ErrorData>(new CreateRoleCommandResult(role.Id));
        }
    }
}