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
    public class GetAllSecurityTipCategoriesQuery : IRequest<PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllSecurityTipCategoriesQueryHandler : IRequestHandler<GetAllSecurityTipCategoriesQuery, PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>>
    {
        private readonly ISecurityTipCategoryRepositoryAsync _securityTipCategoryRepository;
        private readonly IMapper _mapper;
        public GetAllSecurityTipCategoriesQueryHandler(ISecurityTipCategoryRepositoryAsync securityTipCategoryRepository, IMapper mapper)
        {
            _securityTipCategoryRepository = securityTipCategoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>> Handle(GetAllSecurityTipCategoriesQuery request, CancellationToken cancellationToken)
        {
            //var validFilter = _mapper.Map<GetAllSecurityTipCategoriesQueryParameter>(request);
            var securityTipCategories = await _securityTipCategoryRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);
            var securityTipCategoryViewModel = _mapper.Map<IEnumerable<GetAllSecurityTipCategoriesViewModel>>(securityTipCategories);
            return new PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>(securityTipCategoryViewModel, request.PageNumber, request.PageSize);
        }
    }
}
