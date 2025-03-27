// Location: Application/Features/UserProfile/Circle/Commands/ToggleEmergencyContact/ToggleEmergencyContactCommandValidator.cs
using FluentValidation;

namespace Application.Features.UserProfile
{
    public class ToggleEmergencyContactCommandValidator : AbstractValidator<ToggleEmergencyContactCommand>
    {
        public ToggleEmergencyContactCommandValidator()
        {
            RuleFor(v => v.userId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(v => v.memberId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
