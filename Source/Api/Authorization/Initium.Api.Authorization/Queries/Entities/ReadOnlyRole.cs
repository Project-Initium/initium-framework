using System;
using System.Collections.Generic;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Core.Contracts.Queries;

namespace Initium.Api.Authorization.Queries.Entities
{
    public class ReadOnlyRole : IReadOnlyEntity
    {
        private readonly List<ReadOnlyResource> _resources;
        private readonly List<AuthorizedReadOnlyUser> _users;

        public ReadOnlyRole()
        {
            this._resources = new List<ReadOnlyResource>();
            this._users = new List<AuthorizedReadOnlyUser>();
        }
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        
        public IReadOnlyList<ReadOnlyResource> Resources => this._resources.AsReadOnly();
        public IReadOnlyList<AuthorizedReadOnlyUser> Users => this._users.AsReadOnly();
        
        
    }

    public interface IAuthorizedReadOnlyUser
    {
        IReadOnlyList<ReadOnlyRole> Roles { get; }
    }

    public abstract class AuthorizedReadOnlyUser : AuthenticatedReadOnlyUser, IAuthorizedReadOnlyUser
    {
        private readonly List<ReadOnlyRole> _roles = new List<ReadOnlyRole>();

        public IReadOnlyList<ReadOnlyRole> Roles => this._roles.AsReadOnly();
    }
}