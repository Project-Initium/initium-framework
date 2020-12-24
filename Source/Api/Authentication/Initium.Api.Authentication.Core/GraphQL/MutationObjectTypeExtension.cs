using System;
using FairyBread;
using FluentValidation;
using HotChocolate.Types;
using Initium.Api.Authentication.Core.Domain.Commands.UserAggregate;
using Initium.Api.Core.Extensions;
using Initium.Api.Core.GraphQL;
using MediatR;

namespace Initium.Api.Authentication.Core.GraphQL
{
    public class MutationObjectTypeExtension : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name(MutationType.TypeName);
                
            descriptor.Field("createUser")
                .Argument("input", x=> x.Type(typeof(CreateUserInput)).UseValidation())
                .Resolver(async (ctx, token) =>
                {
                    var mediator = ctx.Service<IMediator>();
                    var input = ctx.ArgumentValue<CreateUserInput>("input");
                    var result = await mediator.ThrowOnError(new CreateUserCommand(input.Id, input.EmailAddress, input.FirstName, input.LastName), token);
                    return new ReadOnlyUser();
                }).Type<NonNullType<ReadOnlyUser>>();
        }
    }

    
    
    public class CreateUserInput
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public class Validator : AbstractValidator<CreateUserInput>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id)
                    .NotEmpty();
                this.RuleFor(x => x.EmailAddress)
                    .NotEmpty();
                this.RuleFor(x => x.FirstName)
                    .NotEmpty();
                this.RuleFor(x => x.LastName)
                    .NotEmpty();
            }
        }
    }
}