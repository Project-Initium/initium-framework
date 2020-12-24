// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Domain;

namespace Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate
{
    public sealed class Tenant : Entity, ITenant
    {
        public Tenant(Guid id, string identifier, string name, string connectionString)
        {
            this.Id = id;
            this.Identifier = identifier;
            this.Name = name;
        }

        private Tenant()
        {
        }

        public string Identifier { get; private set; }

        public string Name { get; private set; }

        public DateTime? WhenDisabled { get; private set; }

        public void UpdateDetails(string identifier, string name)
        {
            this.Identifier = identifier;
            this.Name = name;
        }

        public void Enable()
        {
            this.WhenDisabled = null;
        }

        public void Disable(DateTime whenDisabled)
        {
            this.WhenDisabled = whenDisabled;
        }
    }
}