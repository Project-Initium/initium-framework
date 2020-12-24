using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using HotChocolate;
using Initium.Api.Core.Domain;
using Initium.Api.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ResultMonad;
using ErrorCodes = Initium.Api.Core.Domain.ErrorCodes;

namespace Initium.Api.Core.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async domainEvent => { await mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);
        }

        public static async Task DispatchIntegrationEventsAsync(this IMediator mediator, DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.IntegrationEvents != null && x.Entity.IntegrationEvents.Any()).ToList();

            var integrationEvents = domainEntities
                .SelectMany(x => x.Entity.IntegrationEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearIntegrationEvents());

            var tasks = integrationEvents
                .Select(async integrationEvent => { await mediator.Publish(integrationEvent); });

            await Task.WhenAll(tasks);
        }

        public static async Task<TResponse> ThrowOnError<TResponse>(this IMediator mediator, IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await mediator.Send(request, cancellationToken);

                Type resultType = response.GetType().UnderlyingSystemType.Name switch
                {
                    "Result`2" => typeof(Result<,>).MakeGenericType(
                        response.GetType().GetGenericArguments().First(), typeof(ErrorData)),
                    "ResultWithError`1" => typeof(ResultWithError<>).MakeGenericType(typeof(ErrorData)),
                    _ => throw new CustomException("Non result type used as command result")
                };

                var propToCheck = resultType.GetProperty("IsSuccess");

                var propValue = propToCheck.GetValue(response);

                var commandSuccess = (bool)propValue;
                if (commandSuccess)
                {
                    return response;
                }

                var errorDataProperty = resultType.GetProperty("Error");

                var errorData = errorDataProperty.GetValue(response) as ErrorData;

                throw new GraphQLException(new ErrorBuilder()
                .SetMessage("Execution")
                .SetExtension("error-code", errorData.Code)
                .Build());
            }
            catch (ValidationException)
            {
                throw new GraphQLException(new ErrorBuilder()
                    .SetMessage("Execution")
                    .SetExtension("error-code", ErrorCodes.CoreValidation)
                    .Build());
            }
        }
    }
}