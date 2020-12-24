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
    public class CreateInitialUserCommandHandler : IRequestHandler<CreateInitialUserCommand, ResultWithError<ErrorData>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IIdentityProviderClient _identityProvider;

        public CreateInitialUserCommandHandler(IUserRepository userRepository, ILogger logger)
        {
            this._userRepository = userRepository;
            this._logger = logger;
        }

        public async Task<ResultWithError<ErrorData>> Handle(CreateInitialUserCommand request, CancellationToken cancellationToken)
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

        private async Task<ResultWithError<ErrorData>> Process(CreateInitialUserCommand request, CancellationToken cancellationToken)
        {
            var userCreateResult = await this._identityProvider.CreateUser(request.UserId.ToString(), request.EmailAddress);
            if (userCreateResult.IsFailure)
            {
                return ResultWithError.Fail<ErrorData>(new ErrorData(""));
            }

            var passwordResult =
                await this._identityProvider.ChangePassword(userCreateResult.Value.ExternalRef, request.Password);
            if (passwordResult.IsFailure)
            {
                return ResultWithError.Fail<ErrorData>(new ErrorData(""));
            }
            
            this._userRepository.Add(new User(request.UserId, request.EmailAddress, request.FirstName, request.LastName, userCreateResult.Value.ExternalRef));
         
            return ResultWithError.Ok<ErrorData>();

        }
    }
}