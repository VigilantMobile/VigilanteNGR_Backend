using Application.Interfaces.Repositories.AppTroopers.Panic;
using Domain.Entities.AppTroopers.Panic;
using FluentValidation;

namespace Application.Features.UserProfile
{
    public class CreateTrustedPersonsCommandValidator : AbstractValidator<UserCircle>
    {
        private readonly ITrustedPersonRepositoryAsync trustedPersonRepository;

        public CreateTrustedPersonsCommandValidator(ITrustedPersonRepositoryAsync trustedPersonRepository)
        {
            this.trustedPersonRepository = trustedPersonRepository;

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.PhoneNumber)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }
    }
}
