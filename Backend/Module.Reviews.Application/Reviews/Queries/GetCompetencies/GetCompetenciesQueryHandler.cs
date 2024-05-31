using MediatR;
using Microsoft.EntityFrameworkCore;
using Module.Reviews.Contracts.Reviews.Queries.GetCompetencies;
using Module.Reviews.Domain;
using Module.Reviews.Persistence;
using Shared.Exceptions;
using CompetenceViewModel = Module.Explorer.Contracts.Competencies.Queries.GetCompetencies.CompetenceViewModel;

namespace Module.Reviews.Application.Reviews.Queries.GetCompetencies
{
    public class GetCompetenciesQueryHandler : IRequestHandler<GetCompetenciesQuery, List<CompetenceViewModel>>
    {
        private readonly IReviewsDbContext _dbContext;
        private readonly IMediator _mediator;

        public GetCompetenciesQueryHandler(IReviewsDbContext dbContext,
            IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<List<CompetenceViewModel>> Handle(GetCompetenciesQuery request, CancellationToken cancellationToken)
        {
            var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == request.ReviewId, cancellationToken);
            if (review is null)
            {
                throw new NotFoundException(nameof(Review), request.ReviewId);
            }

            return await _mediator.Send(new Explorer.Contracts.Competencies.Queries.GetCompetencies.GetCompetenciesQuery
            {
                JobId = review.JobId
            }, cancellationToken);
        }
    }
}