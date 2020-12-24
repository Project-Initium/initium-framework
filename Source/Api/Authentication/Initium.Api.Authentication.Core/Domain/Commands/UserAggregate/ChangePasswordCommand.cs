using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class ChangePasswordCommand : IRequest<ResultWithError<ErrorData>>
    {
        public ChangePasswordCommand(Guid userId, string newPassword)
        {
            this.UserId = userId;
            this.NewPassword = newPassword;
        }
        
        public Guid UserId { get; }
        
        public string NewPassword { get; }
    }
}