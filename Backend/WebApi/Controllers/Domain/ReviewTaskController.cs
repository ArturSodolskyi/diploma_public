using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Reviews.Contracts.ReviewTasks.Commands.Update;
using WebApi.Models.Domain.ReviewTasks;

namespace WebApi.Controllers.Domain
{
    public class ReviewTaskController : BaseController
    {
        private readonly IMapper _mapper;
        public ReviewTaskController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateReviewTaskRequestModel model)
        {
            var request = _mapper.Map<UpdateReviewTaskCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
