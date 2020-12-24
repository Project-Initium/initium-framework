using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class DisableUserCommand : IRequest<ResultWithError<ErrorData>>
    {
        public DisableUserCommand(Guid userId)
        {
            this.UserId = userId;
        }
        
        public Guid UserId { get; }
    }
}