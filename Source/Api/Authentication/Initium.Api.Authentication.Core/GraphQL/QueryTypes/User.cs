// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;

namespace Initium.Api.Authentication.Core.GraphQL.QueryTypes
{
    public class User
    {
        public Guid Id { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string EmailAddress { get; init; }
    }
}  



