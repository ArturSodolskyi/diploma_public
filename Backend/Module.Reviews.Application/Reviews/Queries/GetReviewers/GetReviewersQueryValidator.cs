using FluentValidation;
using Module.Reviews.Contracts.Reviews.Queries.GetReviewers;

namespace Module.Reviews.Application.Reviews.Queries.GetReviewers
{
    public class GetReviewersQueryValidator : AbstractValidator<GetReviewersQuery>
    {
        public GetReviewersQueryValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}