using FluentValidation;
using Module.Explorer.Contracts.Jobs.Queries.GetJobs;

namespace Module.Explorer.Application.Jobs.Queries.GetJobs
{
    public class GetJobsQueryValidator : AbstractValidator<GetJobsQuery>
    {
        public GetJobsQueryValidator()
        {
            RuleFor(x => x.CompanyId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}