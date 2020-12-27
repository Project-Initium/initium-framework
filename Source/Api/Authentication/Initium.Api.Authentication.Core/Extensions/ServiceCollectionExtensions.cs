// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using Initium.Api.Authentication.Core.DependencyInjection;
using Initium.Api.Authentication.Core.Domain.AggregateModels.UserAggregate;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;
using Initium.Api.Authentication.Core.GraphQL;
using Initium.Api.Authentication.Core.GraphQL.InputTypes;
using Initium.Api.Authentication.Core.Infrastructure;
using Initium.Api.Authentication.Core.Infrastructure.Repositories;
using Initium.Api.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NodaTime;

namespace Initium.Api.Authentication.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static AuthenticationBuilder AddInitiumAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IClock>(SystemClock.Instance);
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddValidatorsFromAssembly(typeof(AuthenticationBuilder).Assembly);
            services.AddMediatR(typeof(AuthenticationBuilder).Assembly);

            services.AddScoped<IIdentityProviderClient, BaseIdentityProviderClient>();
            
            return new AuthenticationBuilder(services);
        }
    }
}