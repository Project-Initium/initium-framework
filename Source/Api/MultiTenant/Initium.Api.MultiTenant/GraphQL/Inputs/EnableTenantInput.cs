using System;
using FluentValidation;

namespace Initium.Api.MultiTenant.GraphQL.Inputs
{
    public class EnableTenantInput
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<EnableTenantInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
            }
        }
    }
}