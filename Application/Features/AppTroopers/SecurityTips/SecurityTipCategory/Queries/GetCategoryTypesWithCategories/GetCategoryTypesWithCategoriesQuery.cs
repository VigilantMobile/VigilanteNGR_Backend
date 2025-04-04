using Application.DTOs.AlertCategories;
using Application.Services.Interfaces.AppTroopers.SecurityTipCategories;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.GetCategoryTypesWithCategories
{
    public class GetCategoryTypesWithCategoriesQuery : IRequest<Response<List<CategoryTypeWithCategoriesDto>>>
    {
    }

    public class GetCategoryTypesWithCategoriesQueryHandler
        : IRequestHandler<GetCategoryTypesWithCategoriesQuery, Response<List<CategoryTypeWithCategoriesDto>>>
    {
        private readonly ISecurityTipCategoryService _securityTipCategoryService;

        public GetCategoryTypesWithCategoriesQueryHandler(ISecurityTipCategoryService securityTipCategoryService)
        {
            _securityTipCategoryService = securityTipCategoryService;
        }

        public async Task<Response<List<CategoryTypeWithCategoriesDto>>> Handle(
            GetCategoryTypesWithCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _securityTipCategoryService.GetCategoryTypesWithCategoriesAsync();
            return new Response<List<CategoryTypeWithCategoriesDto>>(categories, "Success", "Categories retrieved successfully");
        }
    }
}
