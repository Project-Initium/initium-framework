using System.Collections;
using System.Collections.Generic;
using FluentValidation;
using Initium.Api.Authentication.Core.DependencyInjection;
using Initium.Api.Authorization.DependencyInjection;
using Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate;
using Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate;
using Initium.Api.Authorization.Domain.AggregateModels.UserWithRoleAggregate;
using Initium.Api.Authorization.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NodaTime;

namespace Initium.Api.Authorization.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static AuthorizationBuilder AddInitiumAuthorization(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.TryAddSingleton<IClock>(SystemClock.Instance);
            services.AddScoped<IAuthorizedUserRepository, AuthorizedUserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddValidatorsFromAssembly(typeof(AuthorizationBuilder).Assembly);
            services.AddMediatR(typeof(AuthorizationBuilder).Assembly);
            
            return new AuthorizationBuilder(services);
        }
    }
}