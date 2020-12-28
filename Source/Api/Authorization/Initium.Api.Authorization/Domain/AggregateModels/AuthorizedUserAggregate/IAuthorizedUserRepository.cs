using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;

namespace Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate
{
    public interface IAuthorizedUserRepository : IRepository<IAuthorizedUser>
    {
        void Update(IAuthorizedUser authorizedUser);

        Task<Maybe<AuthorizedAuthorizedUser>> Find(Guid userId, CancellationToken cancellationToken = default);
    }
}