using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
