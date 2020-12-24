using System;
using Initium.Api.Core.Domain;
using MediatR;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Domain.Commands.UserAggregate
{
    public class CreateInitialUserCommand : IRequest<ResultWithError<ErrorData>>
    {
        public CreateInitialUserCommand(Guid userId, string emailAddress, string firstName, string lastName, string password)
        {
            this.UserId = userId;
            this.EmailAddress = emailAddress;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        
        public Guid UserId { get; }
        public string EmailAddress { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }

        

       
    }
}