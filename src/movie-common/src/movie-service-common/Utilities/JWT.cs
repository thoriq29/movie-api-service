using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Service.Utilities
{
    public static class JWTHttpContext
    {
        public static string GetAccountId(this HttpContext httpContext)
        {
            var user = httpContext.User;
            return user.FindFirstValue("sub");
        }

        public static string GetClientId(this HttpContext httpContext)
        {
            var user = httpContext.User;
            return user.FindFirstValue("client_id");
        }

        public static ClaimsPrincipal GetUser(this HttpContext httpContext)
        {
            var user = httpContext.User;
            return user;
        }

        public static string? GetAccessToken(this HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            var authHeaderValue = AuthenticationHeaderValue.Parse(authHeader);
            return authHeaderValue.Parameter;
        }
    }
}
