using FluentValidation;

namespace Application.Features.UserProfile
{
    public class ReactivateFriendshipCommandValidator : AbstractValidator<ReactivateFriendshipCommand>
    {
        public ReactivateFriendshipCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.memberId)
                .NotEmpty().WithMessage("FriendId is required.");
        }
    }
}
