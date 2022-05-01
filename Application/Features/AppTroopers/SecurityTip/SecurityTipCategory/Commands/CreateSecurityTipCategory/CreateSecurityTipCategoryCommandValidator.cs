using Application.Interfaces.Repositories.AppTroopers.SecurityTip;
using Domain.Entities.AppTroopers.SecurityTips;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTip.Commands.CreateSecurityTipCategory
{
    public class CreateSecurityTipCategoryCommandValidator : AbstractValidator<SecurityTipCategory>
    {
        private readonly ISecurityTipCategoryRepositorysync securityTipCategoryRepository;

        public CreateSecurityTipCategoryCommandValidator(ISecurityTipCategoryRepositorysync securityTipCategoryRepository)
        {
            this.securityTipCategoryRepository = securityTipCategoryRepository;

            RuleFor(p => p.CategoryName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueCategoryName).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUniqueCategoryName(string CategoryName, CancellationToken cancellationToken)
        {
            return await securityTipCategoryRepository.IsUniqueCategoryName(CategoryName);
        }
    }
}
