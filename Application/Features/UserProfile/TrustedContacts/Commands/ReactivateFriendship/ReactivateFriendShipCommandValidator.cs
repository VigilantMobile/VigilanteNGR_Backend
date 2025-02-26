using FluentValidation;

namespace Application.Features.UserProfile
{
    public class ReactivateFriendshipCommandValidator : AbstractValidator<ReactivateFriendshipCommand>
    {
        public ReactivateFriendshipCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.FriendId)
                .NotEmpty().WithMessage("FriendId is required.");
        }
    }
}
