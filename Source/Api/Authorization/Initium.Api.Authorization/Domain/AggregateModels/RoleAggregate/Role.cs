using System;
using System.Collections.Generic;
using Initium.Api.Core.Domain;

namespace Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate
{
    public sealed class Role : Entity, IRole
    {
        private readonly List<RoleResource> _roleResources;

        public Role(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
            this._roleResources = new List<RoleResource>();
        }
        
        private Role()
        {
            this._roleResources = new List<RoleResource>();
        }
        
        public string Name { get; private set; }

        public IReadOnlyList<RoleResource> RoleResources => this._roleResources.AsReadOnly();

        public void SetResources(List<Guid> resources)
        {
            throw new NotImplementedException();
        }
    }
}