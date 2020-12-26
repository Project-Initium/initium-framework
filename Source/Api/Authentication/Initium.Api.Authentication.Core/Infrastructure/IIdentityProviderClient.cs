// Copyright (c) Project Initium. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MaybeMonad;
using ResultMonad;

namespace Initium.Api.Authentication.Core.Infrastructure
{
    public interface IIdentityProviderClient
    {
        Task<Result<CreateUserResult>> CreateUser(string id, string emailAddress, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok(new CreateUserResult(id)));
        }

        Task<Result> ChangePassword(string id, string password, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok());
        }

        Task<Result> DisableUser(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok());
        }

        Task<Maybe<List<ExternalUser>>> Search(string searchTerm, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Maybe<List<ExternalUser>>.Nothing);
        }

        Task<Result> EnableUser(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Result.Ok());
        }

        public class CreateUserResult
        {
            public CreateUserResult(string externalRef)
            {
                this.ExternalRef = externalRef;
            }

            public string ExternalRef { get; }
        }

        public class ExternalUser
        {
            public ExternalUser(string firstName, string lastName, string emailAddress, string externalRef, string whenSignedIn)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.EmailAddress = emailAddress;
                this.ExternalRef = externalRef;
                this.WhenSignedIn = whenSignedIn;
            }
            public string FirstName { get; }
            public string LastName { get; }
            public string EmailAddress { get; }
            public string ExternalRef { get; }
            public string WhenSignedIn { get; }
        }
    }
}