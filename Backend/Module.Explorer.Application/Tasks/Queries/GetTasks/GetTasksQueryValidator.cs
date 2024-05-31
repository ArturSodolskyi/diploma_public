using FluentValidation;
using Module.Explorer.Contracts.Tasks.Queries.GetTasks;

namespace Module.Explorer.Application.Tasks.Queries.GetTasks
{
    public class GetTasksQueryValidator : AbstractValidator<GetTasksQuery>
    {
        public GetTasksQueryValidator()
        {
            RuleFor(x => x.CompetenceId).GreaterThan(0);
        }
    }
}