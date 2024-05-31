using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Module.Reviews.Contracts.Reviews.Commands.CompleteReview;
using Module.Reviews.Contracts.Reviews.Queries.GetCompetencies;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewDetails;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewResult;
using Module.Reviews.Contracts.Reviews.Queries.GetReviews;
using Module.Reviews.Contracts.Reviews.Queries.GetTasks;
using WebApi.Models.Dashboard.Reviews;

namespace WebApi.Controllers.Dashboard
{
    public class ReviewsController : DashboardBaseController
    {
        private readonly IMapper _mapper;
        public ReviewsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetReviewsQuery();
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(int reviewId)
        {
            var request = new GetReviewDetailsQuery
            {
                ReviewId = reviewId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetCompetencies(int reviewId)
        {
            var request = new GetCompetenciesQuery
            {
                ReviewId = reviewId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(int reviewId, int competenceId)
        {
            var request = new GetTasksQuery
            {
                ReviewId = reviewId,
                CompetenceId = competenceId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviewResult(int reviewId)
        {
            var request = new GetReviewResultQuery
            {
                ReviewId = reviewId
            };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> CompleteReview([FromBody] CompleteReviewRequestModel model)
        {
            var request = _mapper.Map<CompleteReviewCommand>(model);
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
