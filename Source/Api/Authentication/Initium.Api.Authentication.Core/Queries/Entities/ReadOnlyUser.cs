using System;
using Initium.Api.Core.Contracts.Queries;

namespace Initium.Api.Authentication.Core.Queries.Entities
{
    public interface IAuthenticatedReadOnlyUser : IReadOnlyEntity
    {
        public Guid Id { get; }

        public string ExternalRef { get; }
    }
    public abstract class AuthenticatedReadOnlyUser : IAuthenticatedReadOnlyUser
    {
        public Guid Id { get; protected set; }

        public string ExternalRef { get; protected set; }
    }
}