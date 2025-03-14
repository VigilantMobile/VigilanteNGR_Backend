using Application.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    //public class UpdateCustomerProfileUrlCommandValidator : AbstractValidator<CreateProductCommand>
    //{
    //    private readonly IProductRepositoryAsync productRepository;

    //    public UpdateCustomerProfileUrlCommandValidator(IProductRepositoryAsync productRepository)
    //    {
    //        this.productRepository = productRepository;

    //        RuleFor(p => p.Barcode)
    //            .NotEmpty().WithMessage("{PropertyName} is required.")
    //            .NotNull()
    //            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
    //            .MustAsync(IsUniqueBarcode).WithMessage("{PropertyName} already exists.");

    //        RuleFor(p => p.Name)
    //            .NotEmpty().WithMessage("{PropertyName} is required.")
    //            .NotNull()
    //            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
    //    }
    //}
}
