// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;

namespace Initium.Api.MultiTenant.Infrastructure.Settings
{
    public class MultiTenantSettings
    {
        public Guid DefaultTenantId { get; set; }

        public string DefaultIdentifier { get; set; }

        public string DefaultName { get; set; }
    }
}