using Microsoft.AspNetCore.Authorization;
using System;

namespace Service.Utilities.AuthorizationPolicies
{
    public class HasAuthorizationRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }

        public string Scope { get; }

        public string Role { get; }

        public HasAuthorizationRequirement(string scope, string issuer, string role)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Role = role;
        }
    }
}