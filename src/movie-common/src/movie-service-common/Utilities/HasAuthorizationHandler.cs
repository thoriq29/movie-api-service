using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Service.Utilities.AuthorizationPolicies
{
    public class HasAuthorizationHandler : AuthorizationHandler<HasAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasAuthorizationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var scopes = context.User.FindAll(c => c.Type == "scope");
            var roles = context.User.FindFirst(c => c.Type == "role");
            var issuer = context.User.FindFirst(c => c.Issuer == requirement.Issuer).ToString();

            roles ??= new Claim("role", "");

            if (requirement.Role is null)
            {
                if (scopes.Any(s => s.Value == requirement.Scope))
                    context.Succeed(requirement);
            }
            else
            {
                if (scopes.Any(s => s.Value == requirement.Scope) || roles.Value.Equals(requirement.Role))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}