using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authorization.DependencyInjection;
using Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate;
using Initium.Api.Authorization.Domain.AggregateModels.UserWithRoleAggregate;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;
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

            return new AuthorizationBuilder(services);
        }
    }

    public class AuthorizedUserRepository : IAuthorizedUserRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public void Update(IAuthorizedUser authorizedUser)
        {
            throw new NotImplementedException();
        }

        public async Task<Maybe<AuthorizedAuthorizedUser>> Find(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}