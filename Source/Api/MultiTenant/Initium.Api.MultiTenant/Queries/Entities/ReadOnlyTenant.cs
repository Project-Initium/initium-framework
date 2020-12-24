// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Contracts.Queries;

namespace Initium.Api.MultiTenant.Queries.Entities
{
    public class ReadOnlyTenant : IReadOnlyEntity
    {
        public ReadOnlyTenant(Guid id, string identifier, string name, DateTime? whenDisabled)
        {
            this.Id = id;
            this.Identifier = identifier;
            this.Name = name;
            this.WhenDisabled = whenDisabled;
        }
        
        private ReadOnlyTenant()
        {
        }
        
        public Guid Id { get; private set; }

        public string Identifier { get; private set; }

        public string Name { get; private set; }

        public DateTime? WhenDisabled { get; private set; }
    }
}