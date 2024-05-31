using Microsoft.AspNetCore.Mvc;
using Module.Users.Contracts.CurrentUser.Commands.DeleteUser;
using Module.Users.Contracts.CurrentUser.Commands.UpdateCompany;
using Module.Users.Contracts.CurrentUser.Queries.GetUser;

namespace WebApi.Controllers.Domain
{
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetUserQuery();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] int companyId)
        {
            var request = new UpdateCompanyCommand
            {
                CompanyId = companyId
            };
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var request = new DeleteUserCommand();
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
