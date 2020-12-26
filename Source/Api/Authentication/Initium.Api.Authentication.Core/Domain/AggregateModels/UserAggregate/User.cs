// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Domain;

namespace Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate
{
    public sealed class User : Entity, IUser
    {
        public User(Guid id, string externalUserRef)
        {
            this.Id = id;
            this.ExternalUserRef = externalUserRef;
        }

        private User()
        {
        }

        public string ExternalUserRef { get; private set; }
    }
}