using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Domain.Entities;
using Domain.Entities.AppTroopers.SecurityTips;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTipCategory
{
    public class CreateSecurityTipCommandValidator : AbstractValidator<CreateSecurityTipCommand>
    {
        private readonly ISecurityTipRepositoryAsync _securityTipRepositoryAsync;

        public CreateSecurityTipCommandValidator(ISecurityTipRepositoryAsync securityTipRepositoryAsync)
        {
            this._securityTipRepositoryAsync = securityTipRepositoryAsync;

            RuleFor(p => p.SourceId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

            RuleFor(p => p.BroadcasterUserId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();

            RuleFor(p => p.CategoryId)
             .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.coordinates)
             .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

            RuleFor(p => p.IncidentCoordinates)
            .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

            RuleFor(p => p.Subject)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Description)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull()
             .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
