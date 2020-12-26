// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;

namespace Initium.Api.Authentication.Core.GraphQL.QueryTypes
{
    public class User
    {
        public User(Guid id, string firstName, string lastName, string emailAddress)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailAddress = emailAddress;
        }

        public Guid Id { get; }

        private string FirstName { get; }

        private string LastName { get; }

        private string EmailAddress { get; }
    }
}