using System;
using FluentValidation;

namespace Initium.Api.MultiTenant.GraphQL.Inputs
{
    public class DisableTenantInput
    {
        public Guid Id { get; set; }

        public class Validator : AbstractValidator<DisableTenantInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
            }
        }
    }
}