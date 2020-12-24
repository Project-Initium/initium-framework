// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using Initium.Api.Core.Contracts.Domain;

namespace Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate
{
    public interface IUser : IAggregateRoot, IEntity
    {
        public string EmailAddress { get; }
        
        public string FirstName { get; }
        
        public string LastName { get; }
        
        string ExternalUserRef { get; }
    }
}