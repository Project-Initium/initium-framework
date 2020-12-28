using System.Collections.Generic;
using Initium.Api.Authorization.Domain.AggregateModels.UserWithRoleAggregate;
using Initium.Api.Core.Domain;

namespace Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate
{
    public class AuthorizedAuthorizedUser : Entity, IAuthorizedUser
    {
        private readonly List<UserRole> _userRoles;

        private AuthorizedAuthorizedUser()
        {
            this._userRoles = new List<UserRole>();
        }

        public IReadOnlyList<UserRole> UserRoles => this._userRoles;
    }
}