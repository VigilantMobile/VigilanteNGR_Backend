using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.Panic.Commands.CreateTrustedPerson
{
    public class CreateTrustedPersonsCommandValidator : AbstractValidator<TrustedPerson>
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
