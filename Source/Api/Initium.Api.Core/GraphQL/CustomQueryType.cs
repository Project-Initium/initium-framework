using HotChocolate.Types;

namespace Initium.Api.Core.GraphQL
{
    public class CustomQueryType : ObjectType
    {
        public const string TypeName = "Queries";

        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(TypeName);
        }
    }
}