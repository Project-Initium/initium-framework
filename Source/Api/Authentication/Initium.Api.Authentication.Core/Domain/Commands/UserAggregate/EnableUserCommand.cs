using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class EnableUserCommand : IRequest<ResultWithError<ErrorData>>
    {
        public Guid UserId { get; }
    }
}