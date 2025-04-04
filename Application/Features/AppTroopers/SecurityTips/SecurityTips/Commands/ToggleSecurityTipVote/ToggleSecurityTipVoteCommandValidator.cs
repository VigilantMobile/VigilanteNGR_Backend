using FluentValidation;

namespace Application.Features.AppTroopers.SecurityTips.Commands.ToggleSecurityTipVote
{
    public class ToggleSecurityTipVoteCommandValidator : AbstractValidator<ToggleSecurityTipVoteCommand>
    {
        public ToggleSecurityTipVoteCommandValidator()
        {

            RuleFor(v => v.SecurityTipId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(v => v.VoteType)
                .IsInEnum().WithMessage("{PropertyName} must be a valid vote type.");
        }
    }
}