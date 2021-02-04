// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using HotChocolate.Execution.Configuration;
using Initium.Api.Authentication.Core.GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authentication.Core.Extensions
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder RegisterAuthentication(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddType<MutationObjectTypeExtension>();
        }
    }
}