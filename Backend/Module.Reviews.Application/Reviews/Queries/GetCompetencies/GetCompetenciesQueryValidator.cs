using FluentValidation;
using Module.Reviews.Contracts.Reviews.Queries.GetCompetencies;

namespace Module.Reviews.Application.Reviews.Queries.GetCompetencies
{
    public class GetCompetenciesQueryValidator : AbstractValidator<GetCompetenciesQuery>
    {
        public GetCompetenciesQueryValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0);
        }
    }
}