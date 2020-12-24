using System;
using Initium.Api.Core.Domain;

namespace Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity, IUser
    {
        public User(Guid id, string emailAddress, string firstName, string lastName, string externalUserRef)
        {
            this.Id = id;
            this.EmailAddress = emailAddress;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ExternalUserRef = externalUserRef;
        }
        
        private User()
        {
        
        }

        public string EmailAddress { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string ExternalUserRef { get; private set; }

    }
}