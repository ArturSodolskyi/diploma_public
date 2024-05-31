using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Reviews.Contracts.Reviews.Commands.Create;
using WebApi.Models.Domain.Reviews;

namespace WebApi.Controllers.Domain
{
    public class ReviewController : BaseController
    {
        private readonly IMapper _mapper;
        public ReviewController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequestModel model)
        {
            var request = _mapper.Map<CreateReviewCommand>(model);
            var id = await Mediator.Send(request);
            return Ok(id);
        }
    }
}
