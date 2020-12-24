using System.Collections.Generic;
using System.Reflection;

namespace Initium.Api.Core
{
    public class ServiceRegistrationResult
    {
        public ServiceRegistrationResult(IReadOnlyList<Assembly> assembliesRequiringValidation, IReadOnlyList<Assembly> assembliesRequiringMediation)
        {
            this.AssembliesRequiringValidation = assembliesRequiringValidation;
            this.AssembliesRequiringMediation = assembliesRequiringMediation;
        }
        public IReadOnlyList<Assembly> AssembliesRequiringValidation { get; }
        public IReadOnlyList<Assembly> AssembliesRequiringMediation { get; }
    }
}