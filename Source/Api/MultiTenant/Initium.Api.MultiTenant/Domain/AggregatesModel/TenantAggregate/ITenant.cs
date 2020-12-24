// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using Initium.Api.Core.Contracts.Domain;

namespace Initium.Api.MultiTenant.Domain.AggregatesModel.TenantAggregate
{
    public interface ITenant : IAggregateRoot, IEntity
    {
        string Identifier { get; }

        string Name { get; }

        DateTime? WhenDisabled { get; }

        void UpdateDetails(string identifier, string name);

        void Enable();

        void Disable(DateTime whenDisabled);
    }
}