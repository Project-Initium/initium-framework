using System.Collections.Generic;
using System.Reflection;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;
using Initium.Api.Authentication.Core.GraphQL;
using Initium.Api.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Initium.Api.Authentication.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceRegistrationResult RegisterMultiTenant(this IServiceCollection services, IConfiguration configuration)
        {
            
            return new ServiceRegistrationResult(
                new List<Assembly>
                {
                    typeof(CreateUserInput.Validator).Assembly
                },
                new List<Assembly>
                {
                    typeof(CreateInitialUserCommand).Assembly
                });
        }
    }
}