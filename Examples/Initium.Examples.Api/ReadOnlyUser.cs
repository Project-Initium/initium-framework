using System;
using System.Collections.Generic;
using Initium.Api.Authentication.Core.Queries.Entities;

namespace Initium.Examples.Api
{
    public class ReadOnlyUser : IReadOnlyUser
    {
        public Guid Id { get; set; }

        public string ExternalRef { get; set; }
    }
}