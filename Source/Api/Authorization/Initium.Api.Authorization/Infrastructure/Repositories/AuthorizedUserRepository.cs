using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Authorization.Domain.AggregateModels.AuthorizedUserAggregate;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;

namespace Initium.Api.Authorization.Extensions
{
    public class AuthorizedUserRepository : IAuthorizedUserRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public void Update(IAuthorizedUser authorizedUser)
        {
            throw new NotImplementedException();
        }

        public async Task<Maybe<AuthorizedAuthorizedUser>> Find(Guid userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}