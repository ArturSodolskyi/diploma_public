using System.Security.Claims;

namespace Shared.Accessors
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
        int UserId { get; }
        string Email { get; }
        bool IsAdministrator { get; }
    }
}
