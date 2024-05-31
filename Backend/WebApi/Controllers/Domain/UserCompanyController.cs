using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Companies.Contracts.UserCompanies.Commands.DeleteUserCompany;
using Module.Companies.Contracts.UserCompanies.Commands.UpdateRole;
using WebApi.Models.Domain.UserCompanies;

namespace WebApi.Controllers.Domain
{
    public class UserCompanyController : BaseController
    {
        private readonly IMapper _mapper;
        public UserCompanyController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequestModel model)
        {
            var request = _mapper.Map<UpdateRoleCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int userId, int companyId)
        {
            var request = new DeleteUserCompanyCommand
            {
                UserId = userId,
                CompanyId = companyId
            };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
