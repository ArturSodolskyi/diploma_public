using System.Security.Claims;

namespace Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var id = principal.GetClaim(ClaimTypes.NameIdentifier);
            return id is null ? 0 : int.Parse(id);
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.GetClaim(ClaimTypes.Email)!;
        }

        private static string? GetClaim(this ClaimsPrincipal principal, string type)
        {
            return principal.Claims.First(i => i.Type == type).Value;
        }
    }
}
