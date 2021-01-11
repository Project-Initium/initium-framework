// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Authentication.Core.Queries.Entities;

namespace Initium.Examples.Api
{
    public class ReadOnlyUser : IReadOnlyUser
    {
        public Guid Id { get; set; }

        public string ExternalRef { get; set; }
    }
}