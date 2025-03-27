using FluentValidation;

namespace Application.Features.AppTroopers.SecurityTips.Commands.ToggleCommentVote
{
    public class ToggleCommentVoteCommandValidator : AbstractValidator<ToggleCommentVoteCommand>
    {
        public ToggleCommentVoteCommandValidator()
        {

            RuleFor(v => v.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(v => v.VoteType)
                .IsInEnum().WithMessage("{PropertyName} must be a valid vote type.");
        }
    }
}
