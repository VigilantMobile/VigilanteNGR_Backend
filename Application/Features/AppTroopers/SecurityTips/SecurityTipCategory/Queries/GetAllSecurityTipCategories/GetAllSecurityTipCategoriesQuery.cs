using Application.Features.AppTroopers.SecurityTips.GetAllSecurityTipCategories;
using Application.Features.AppTroopers.SecurityTips.Queries.GetAllSecurityTipCategories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTips;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTips.GetAllSecurityTipCategories
{
    public class GetAllSecurityTipCategoriesQuery : IRequest<Response<IEnumerable<GetAllSecurityTipCategoriesViewModel>>>
    {
    }

    public class GetAllSecurityTipCategoriesQueryHandler
        : IRequestHandler<GetAllSecurityTipCategoriesQuery, Response<IEnumerable<GetAllSecurityTipCategoriesViewModel>>>
    {
        private readonly ISecurityTipCategoryRepositoryAsync _securityTipCategoryRepository;
        private readonly IMapper _mapper;

        public GetAllSecurityTipCategoriesQueryHandler(ISecurityTipCategoryRepositoryAsync securityTipCategoryRepository, IMapper mapper)
        {
            _securityTipCategoryRepository = securityTipCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllSecurityTipCategoriesViewModel>>> Handle(
            GetAllSecurityTipCategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var securityTipCategories = await _securityTipCategoryRepository.GetAllAsync();
            var securityTipCategoryViewModel = _mapper.Map<IEnumerable<GetAllSecurityTipCategoriesViewModel>>(securityTipCategories);
            return new Response<IEnumerable<GetAllSecurityTipCategoriesViewModel>>(securityTipCategoryViewModel, "Success", "Categories retrieved successfully");
        }
    }
}