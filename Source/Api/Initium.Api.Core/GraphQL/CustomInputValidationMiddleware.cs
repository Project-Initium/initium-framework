using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FairyBread;
using FluentValidation;
using FluentValidation.Results;
using HotChocolate;
using HotChocolate.Resolvers;

namespace Initium.Api.Core.GraphQL
{
    public class CustomInputValidationMiddleware
    {
        private readonly FieldDelegate _next;
        private readonly IFairyBreadOptions _options;
        private readonly IValidatorProvider _validatorProvider;
        private readonly IValidationResultHandler _validationResultHandler;

        public CustomInputValidationMiddleware(FieldDelegate next,
            IFairyBreadOptions options,
            IValidatorProvider validatorProvider,
            IValidationResultHandler validationResultHandler)
        {
            this._next = next;
            this._options = options;
            this._validatorProvider = validatorProvider;
            this._validationResultHandler = validationResultHandler;
        }

        public async Task InvokeAsync(IMiddlewareContext context)
        {
            var arguments = context.Field.Arguments;

            var validationResults = new List<ValidationResult>();

            foreach (var argument in arguments)
            {
                if (!this._options.ShouldValidate(context, argument))
                {
                    continue;
                }

                var resolvedValidators = this._validatorProvider.GetValidators(context, argument);
                try
                {
                    var value = context.ArgumentValue<object>(argument.Name);
                    foreach (var resolvedValidator in resolvedValidators)
                    {
                        var validationContext = new ValidationContext<object>(value);
                        var validationResult = await resolvedValidator.Validator.ValidateAsync(validationContext, context.RequestAborted);
                        if (validationResult != null)
                        {
                            validationResults.Add(validationResult);
                            this._validationResultHandler.Handle(context, validationResult);
                        }
                    }
                }
                finally
                {
                    foreach (var resolvedValidator in resolvedValidators)
                    {
                        resolvedValidator.Scope?.Dispose();
                    }
                }
            }

            var invalidValidationResults = validationResults.Where(r => !r.IsValid);
            if (invalidValidationResults.Any())
            {
                this.OnInvalid(context, invalidValidationResults);
            }

            await this._next(context);
        }

        protected virtual void OnInvalid(IMiddlewareContext context, IEnumerable<ValidationResult> invalidValidationResults)
        {
            throw new GraphQLException(
                invalidValidationResults.SelectMany(result => result.Errors.Select(failure => 
                    new ErrorBuilder()
                        .SetMessage("validation")
                        .SetExtension("property", failure.PropertyName)
                        .SetExtension("error-message", failure.ErrorMessage)
                        .SetExtension("error-code", failure.ErrorCode)
                        .Build()
                ))
                
            );

        }
    }
}