using FluentValidation;
using Module.Explorer.Contracts.Competencies.Queries.GetCompetencies;

namespace Module.Explorer.Application.Competencies.Queries.GetCompetencies
{
    public class GetCompetenciesQueryValidator : AbstractValidator<GetCompetenciesQuery>
    {
        public GetCompetenciesQueryValidator()
        {
            RuleFor(x => x.JobId).GreaterThan(0);
        }
    }
}