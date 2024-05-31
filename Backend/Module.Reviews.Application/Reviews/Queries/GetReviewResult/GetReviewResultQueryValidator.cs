using FluentValidation;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewResult;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewResult
{
    public class GetReviewResultQueryValidator : AbstractValidator<GetReviewResultQuery>
    {
        public GetReviewResultQueryValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0);
        }
    }
}