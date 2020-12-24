using System.Threading.Tasks;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Infrastructure
{
    public interface IIdentityProviderClient
    {
        Task<Result<CreateUserResult>> CreateUser(string id, string emailAddress)
        {
            return Task.FromResult(Result.Ok(new CreateUserResult(id)));
        }

        public class CreateUserResult
        {
            public CreateUserResult(string externalRef)
            {
                this.ExternalRef = externalRef;
            }
            public string ExternalRef { get; }
        }

        Task<Result> ChangePassword(string id, string password)
        {
            return Task.FromResult(Result.Ok());
        }

        Task<Result> DisableUser(string id)
        {
            return Task.FromResult(Result.Ok());
        }
        
        Task<Result> EnableUser(string id)
        {
            return Task.FromResult(Result.Ok());
        }
    }
}