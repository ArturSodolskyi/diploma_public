using Microsoft.AspNetCore.Http;
using Shared.Extensions;
using Shared.Models;
using System.Security.Claims;

namespace Shared.Accessors
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public ClaimsPrincipal User => _accessor.HttpContext.User;
        public bool IsAdministrator => User.Identity is not null
            && User.Identity.IsAuthenticated
            && User.IsInRole(RoleEnum.Administrator.GetEnumDescription());

        public int UserId => User.Identity is not null && User.Identity.IsAuthenticated
            ? User.GetUserId()
            : 0;

        public string Email => User.Identity is not null && User.Identity.IsAuthenticated
            ? User.GetEmail()
            : "";
    }
}
