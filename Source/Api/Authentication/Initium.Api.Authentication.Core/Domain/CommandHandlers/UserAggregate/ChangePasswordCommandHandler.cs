// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;
using Initium.Api.Authentication.Core.Infrastructure;
using Initium.Api.Core.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.CommandHandlers.UserAggregate
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ResultWithError<ErrorData>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IIdentityProviderClient _identityProvider;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            ILogger<ChangePasswordCommandHandler> logger,
            IIdentityProviderClient identityProvider)
        {
            this._userRepository = userRepository;
            this._logger = logger;
            this._identityProvider = identityProvider;
        }

        public async Task<ResultWithError<ErrorData>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await this.Process(request, cancellationToken);
            var dbResult = await this._userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (dbResult)
            {
                return result;
            }

            this._logger.LogDebug("Failed saving changes.");
            return ResultWithError.Fail(new ErrorData(
                ErrorCodes.SavingChanges, "Failed To Save Database"));
        }

        private async Task<ResultWithError<ErrorData>> Process(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var userResult = await this._userRepository.Find(request.UserId, cancellationToken);

            if (userResult.HasNoValue)
            {
                return ResultWithError.Fail<ErrorData>(new ErrorData(""));
            }

            var result = await this._identityProvider.ChangePassword(userResult.Value.ExternalUserRef, request.NewPassword);

            return result.IsSuccess ? ResultWithError.Ok<ErrorData>() : ResultWithError.Fail<ErrorData>(new ErrorData(""));
        }
    }
}