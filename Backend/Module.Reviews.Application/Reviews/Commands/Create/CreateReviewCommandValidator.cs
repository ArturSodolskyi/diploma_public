using FluentValidation;
using Module.Reviews.Contracts.Reviews.Commands.Create;

namespace Module.Reviews.Application.Reviews.Commands.Create
{
    public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            RuleFor(x => x.JobId).GreaterThan(0);
            RuleFor(x => x.RevieweeId).GreaterThan(0);
            RuleFor(x => x.ReviewerId).GreaterThan(0);
        }
    }
}
