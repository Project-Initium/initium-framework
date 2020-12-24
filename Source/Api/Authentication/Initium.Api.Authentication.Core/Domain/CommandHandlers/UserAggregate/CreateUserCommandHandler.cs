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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResultWithError<ErrorData>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IIdentityProviderClient _identityProvider;

        public CreateUserCommandHandler(IUserRepository userRepository, ILogger<CreateUserCommandHandler> logger, IIdentityProviderClient identityProvider)
        {
            this._userRepository = userRepository;
            this._logger = logger;
            this._identityProvider = identityProvider;
        }

        public async Task<ResultWithError<ErrorData>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

        private async Task<ResultWithError<ErrorData>> Process(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await this._identityProvider.CreateUser(request.UserId.ToString(), request.EmailAddress);
            if (result.IsFailure)
            {
                return ResultWithError.Fail<ErrorData>(new ErrorData(""));
            }

            this._userRepository.Add(new User(request.UserId, request.EmailAddress, request.FirstName, request.LastName, result.Value.ExternalRef));
         
            return ResultWithError.Ok<ErrorData>();
        }
    }
}