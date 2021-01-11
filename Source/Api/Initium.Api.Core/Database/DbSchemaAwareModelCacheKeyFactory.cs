// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Initium.Api.Core.Database
{
    public class DbSchemaAwareModelCacheKeyFactory
        : IModelCacheKeyFactory
    {
        public object Create(DbContext context)
        {
            return new
            {
                Type = context.GetType(),
                Schema = context is ISchemaIdentifier schema ? schema.SelectedSchema : null,
            };
        }
    }
}