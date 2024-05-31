using FluentValidation;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewDetails;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewDetails
{
    public class GetReviewDetailsQueryValidator : AbstractValidator<GetReviewDetailsQuery>
    {
        public GetReviewDetailsQueryValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0);
        }
    }
}