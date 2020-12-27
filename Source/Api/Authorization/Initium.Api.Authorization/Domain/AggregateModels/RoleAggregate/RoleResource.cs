using System;
using Initium.Api.Core.Domain;

namespace Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate
{
    public sealed class RoleResource : Entity
    {
        public RoleResource(Guid id)
        {
            this.Id = id;
        }
    }
}