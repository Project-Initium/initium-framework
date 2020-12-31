using System;
using Initium.Api.Authentication.Core.Queries.Entities;

namespace Initium.Examples.Api
{
    public class ReadOnlyUser : IAuthenticatedReadOnlyUser
    {
        public Guid Id { get; set; }

        public string ExternalRef { get; set; }
    }
}