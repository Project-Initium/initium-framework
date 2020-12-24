using FluentValidation;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;

namespace Initium.Api.Authentication.Core.Domain.CommandValidators.UserAggregate
{
    public class AddUserFromProviderCommandValidator : AbstractValidator<AddUserFromProviderCommand>
    {
        public AddUserFromProviderCommandValidator()
        {
            this.RuleFor(x => x.UserId)
                .NotEmpty();
            this.RuleFor(x => x.ExternalRef)
                .NotEmpty();
        }
    }
}