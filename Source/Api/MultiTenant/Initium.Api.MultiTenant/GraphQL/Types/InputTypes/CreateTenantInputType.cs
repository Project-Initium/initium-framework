using HotChocolate.Types;
using Initium.Api.MultiTenant.GraphQL.Inputs;

namespace Initium.Api.MultiTenant.GraphQL.Types.InputTypes
{
    public class CreateTenantInputType : InputObjectType<CreateTenantInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<CreateTenantInput> descriptor)
        {
            base.Configure(descriptor);
            this.Name = "CreateTenant";
        }
    }
}