using System;
using Initium.Api.Core.Contracts.Queries;

namespace Initium.Api.Authentication.Core.Queries.Entities
{
    public class ReadOnlyUser : IReadOnlyEntity
    {
        private ReadOnlyUser()
        {
        }

        public Guid Id { get; }

        public string ExternalRef { get; }
    }
}