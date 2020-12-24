using HotChocolate.Types;

namespace Initium.Api.Core.GraphQL
{
    public class MutationType : ObjectType
    {
        public const string TypeName = "Mutations";

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(TypeName);
        }
    }
}