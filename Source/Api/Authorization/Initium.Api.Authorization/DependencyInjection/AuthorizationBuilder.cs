using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authorization.DependencyInjection
{
    public class AuthorizationBuilder
    {
        public AuthorizationBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }
    }
}