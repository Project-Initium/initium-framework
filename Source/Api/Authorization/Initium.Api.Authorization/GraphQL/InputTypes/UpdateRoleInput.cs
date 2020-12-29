using System;
using System.Collections.Generic;

namespace Initium.Api.Authorization.GraphQL.InputTypes
{
    public class UpdateRoleInput
    {
        public Guid RoleId { get; set; }

        public string Name { get; set; }

        public List<Guid> Resources { get; set; }
    }
}