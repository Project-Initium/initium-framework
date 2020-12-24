using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class AddUserFromProviderCommand : IRequest<ResultWithError<ErrorData>>
    {
        public AddUserFromProviderCommand(Guid userId, string externalRef)
        {
            this.UserId = userId;
            this.ExternalRef = externalRef;
        }
        
        public Guid UserId { get; }
        public string ExternalRef { get; }
    }
}