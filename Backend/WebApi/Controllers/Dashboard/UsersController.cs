using Microsoft.AspNetCore.Mvc;
using Module.Users.Contracts.Users.Commands.DeleteUser;
using Module.Users.Contracts.Users.Commands.UpdateUserRole;
using Module.Users.Contracts.Users.Queries.GetUsers;
using Shared.Models;

namespace WebApi.Controllers.Dashboard
{
    public class UsersController : DashboardBaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetUsersQuery();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole(int userId, RoleEnum role)
        {
            var request = new UpdateUserRoleCommand
            {
                UserId = userId,
                Role = role
            };
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var request = new DeleteUserCommand
            {
                UserId = userId,
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}

