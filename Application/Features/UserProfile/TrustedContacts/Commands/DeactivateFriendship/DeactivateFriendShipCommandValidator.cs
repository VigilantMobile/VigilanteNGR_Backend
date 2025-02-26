using FluentValidation;

namespace Application.Features.UserProfile
{
    public class DeactivateFriendshipCommandValidator : AbstractValidator<DeactivateFriendshipCommand>
    {
        public DeactivateFriendshipCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.FriendId)
                .NotEmpty().WithMessage("FriendId is required.");
        }
    }
}
