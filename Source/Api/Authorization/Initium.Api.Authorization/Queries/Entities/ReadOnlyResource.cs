using System;
using System.Collections.Generic;
using Initium.Api.Core.Contracts.Queries;

namespace Initium.Api.Authorization.Queries.Entities
{
    public class ReadOnlyResource : IReadOnlyEntity
    {
        private readonly List<ReadOnlyResource> _resources;
        private readonly List<ReadOnlyRole> _roles;
        
        private ReadOnlyResource()
        {
            this._resources = new List<ReadOnlyResource>();
            this._roles = new List<ReadOnlyRole>();
        }
        
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string NormalizedName { get; private set; }
        
        public ReadOnlyResource Resource { get; private set; }
        public Guid? ParentResourceId { get; private set; }

        public IReadOnlyList<ReadOnlyResource> Resources => this._resources.AsReadOnly();
        public IReadOnlyList<ReadOnlyRole> Roles => this._roles.AsReadOnly();

    }
}