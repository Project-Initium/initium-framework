// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Initium.Api.Authentication.Core.Queries.Entities;
using Initium.Api.Authorization.Queries.Entities;

namespace Initium.Examples.Api.Infrastructure.Entities
{
    public class ReadOnlyUser : AuthorizedReadOnlyUser
    {
       private ReadOnlyUser()
        {
       
        }
        
       
    }
}