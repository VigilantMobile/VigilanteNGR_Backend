using Application.Interfaces.Repositories.Location;
using Domain.Entities.LocationEntities;
using FluentValidation;

namespace Application.Features.Location
{
    public class CreateTownCommandValidator : AbstractValidator<Town>
    {
        private readonly ITownRepositoryAsync townRepositoryAsync;

        public CreateTownCommandValidator(ITownRepositoryAsync townRepositoryAsync)
        {
            this.townRepositoryAsync = townRepositoryAsync;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");


            RuleFor(p => p.LGAId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }

    }
}
