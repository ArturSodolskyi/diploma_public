using FluentValidation;
using Module.Reviews.Contracts.Reviews.Queries.GetUserReviews;

namespace Module.Reviews.Application.Reviews.Queries.GetUserReviews
{
    public class GetUserReviewsQueryValidator : AbstractValidator<GetUserReviewsQuery>
    {
        public GetUserReviewsQueryValidator()
        {
            RuleFor(x => x.UserId).GreaterThan(0);
            RuleFor(x => x.CompanyId).GreaterThan(0);
        }
    }
}