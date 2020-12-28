using System.Collections.Generic;
using Initium.Api.Authorization.Domain.AggregateModels.UserWithRoleAggregate;
using Initium.Api.Core.Contracts.Domain;

namespace Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate
{
    public interface IAuthorizedUser : IAggregateRoot, IEntity
    {
        IReadOnlyList<UserRole> UserRoles { get; }
    }
}