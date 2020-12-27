using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authentication.Core.DependencyInjection
{
    public class AuthenticationBuilder
    {
        public AuthenticationBuilder(IServiceCollection services)
        {
            this.Services = services;
        }

        public IServiceCollection Services { get; }
    }
}