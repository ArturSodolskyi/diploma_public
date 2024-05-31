using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Companies.Application.UserCompanyInvitations.Queries.GetCompanyInvitations;
using Module.Companies.Contracts.Companies.Queries.GetCompanies;
using Module.Companies.Contracts.Companies.Queries.GetUsers;
using Module.Companies.Contracts.UserCompanyInvitations.Commands.RespondInvitation;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewers;
using Module.Reviews.Contracts.Reviews.Queries.GetUserReviews;
using WebApi.Models.Dashboard.Companies;

namespace WebApi.Controllers.Dashboard
{
    public class CompaniesController : DashboardBaseController
    {
        private readonly IMapper _mapper;
        public CompaniesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetCompaniesQuery();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetInvitations()
        {
            var request = new GetCompanyInvitationsQuery();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int companyId)
        {
            var request = new GetCompanyUsersQuery
            {
                CompanyId = companyId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetJobs([FromBody] GetJobsRequestModel model)
        {
            var request = _mapper.Map<GetJobsQuery>(model);
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetReviewers([FromBody] GetReviewersRequestModel model)
        {
            var request = _mapper.Map<GetReviewersQuery>(model);
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserReviews(int userId, int companyId)
        {
            var request = new GetUserReviewsQuery
            {
                UserId = userId,
                CompanyId = companyId,
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RespondInvitation([FromBody] RespondInvitationRequestModel model)
        {
            var request = _mapper.Map<RespondInvitationCommand>(model);
            await Mediator.Send(request);
            return Ok();
        }
    }
}
