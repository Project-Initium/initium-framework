using HotChocolate.Types;
using Initium.Api.Authentication.Core.Queries.Entities;

namespace Initium.Api.Authentication.Core.GraphQL.EntityTypes
{
    public class AuthenticatedReadOnlyUserInterfaceType : InterfaceType<IAuthenticatedReadOnlyUser>
    {
    }
}