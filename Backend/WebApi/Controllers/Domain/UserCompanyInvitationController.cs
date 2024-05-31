using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.Create;
using WebApi.Models.Domain.UserCompanyInvitations;

namespace WebApi.Controllers.Domain
{
    public class UserCompanyInvitationController : BaseController
    {
        private readonly IMapper _mapper;
        public UserCompanyInvitationController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCompanyInvitationRequestModel model)
        {
            var request = _mapper.Map<CreateUserCompanyInvitationCommand>(model);
            await Mediator.Send(request);
            return Ok();
        }
    }
}

