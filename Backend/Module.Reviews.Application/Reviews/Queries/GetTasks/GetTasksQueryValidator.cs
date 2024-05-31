using FluentValidation;
using Module.Reviews.Contracts.Reviews.Queries.GetTasks;

namespace Module.Reviews.Application.Reviews.Queries.GetTasks
{
    public class GetTasksQueryValidator : AbstractValidator<GetTasksQuery>
    {
        public GetTasksQueryValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0);
            RuleFor(x => x.CompetenceId).GreaterThan(0);
        }
    }
}