using FluentValidation;
using Module.Reviews.Contracts.ReviewTasks.Commands.Update;

namespace Module.Reviews.Application.ReviewTasks.Commands.Update
{
    public class UpdateReviewTaskCommandValidator : AbstractValidator<UpdateReviewTaskCommand>
    {
        public UpdateReviewTaskCommandValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0);
            RuleFor(x => x.TaskId).GreaterThan(0);
            RuleFor(x => x.Value).InclusiveBetween(0, 100);
        }
    }
}