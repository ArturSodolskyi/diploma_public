using FluentValidation;
using Module.Reviews.Contracts.Reviews.Commands.CompleteReview;

namespace Module.Reviews.Application.Reviews.Commands.CompleteReview
{
    public class CompleteReviewCommandValidator : AbstractValidator<CompleteReviewCommand>
    {
        public CompleteReviewCommandValidator()
        {
            RuleFor(x => x.ReviewId).GreaterThan(0);
            RuleFor(x => x.Comment).NotEmpty();
        }
    }
}