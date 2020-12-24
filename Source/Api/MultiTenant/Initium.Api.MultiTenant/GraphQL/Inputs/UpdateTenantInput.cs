using System;
using FluentValidation;

namespace Initium.Api.MultiTenant.GraphQL.Inputs
{
    public class UpdateTenantInput
    {
        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }

        public class Validator : AbstractValidator<UpdateTenantInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
                this.RuleFor(x => x.Identifier)
                    .NotEmpty();
                this.RuleFor(x => x.Name)
                    .NotEmpty();
            }
        }
    }
}