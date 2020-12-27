using System;
using System.Collections.Generic;
using Initium.Api.Core.Contracts.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate
{
    public interface IRole : IAggregateRoot, IEntity
    {
        string Name { get; }
        IReadOnlyList<RoleResource> RoleResources { get; }

        void SetResources(List<Guid> resources);
    }
}