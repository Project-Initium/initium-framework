using System;
using System.Threading;
using System.Threading.Tasks;
using Initium.Api.Core.Contracts.Domain;
using MaybeMonad;

namespace Initium.Api.Authorization.Domain.AggregateModels.RoleAggregate
{
    public interface IRoleRepository : IRepository<IRole>
    {
        IRole Add(IRole role);
        void Update(IRole role);
        Task<Maybe<IRole>> Find(Guid roleId, CancellationToken cancellationToken = default);
    }
}