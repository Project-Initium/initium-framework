using System;
using Initium.Api.Core.Contracts.Queries;

namespace Initium.Api.Authentication.Core.Queries.Entities
{
    public interface IAuthenticatedReadOnlyUser : IReadOnlyEntity
    {
        public Guid Id { get; set; }

        public string ExternalRef { get; set; }
    }
}