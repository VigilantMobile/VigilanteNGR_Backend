using Application.Features.AppTroopers.SecurityTip.Queries.GetAllSecurityTipCategories;
using Application.Filters;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.SecurityTip;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.SecurityTip.GetAllSecurityTipCategories
{
    public class GetAllSecurityTipCategoriesQuery : IRequest<PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllSecurityTipCategoriesQueryHandler : IRequestHandler<GetAllSecurityTipCategoriesQuery, PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>>
    {
        private readonly ISecurityTipCategoryRepositorysync _securityTipCategoryRepository;
        private readonly IMapper _mapper;
        public GetAllSecurityTipCategoriesQueryHandler(ISecurityTipCategoryRepositorysync securityTipCategoryRepository, IMapper mapper)
        {
            _securityTipCategoryRepository = securityTipCategoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>> Handle(GetAllSecurityTipCategoriesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllSecurityTipCategoriesParameter>(request);
            var securityTipCategories = await _securityTipCategoryRepository.GetPagedReponseAsync(validFilter.PageNumber,validFilter.PageSize);
            var securityTipCategoryViewModel = _mapper.Map<IEnumerable<GetAllSecurityTipCategoriesViewModel>>(securityTipCategories);
            return new PagedResponse<IEnumerable<GetAllSecurityTipCategoriesViewModel>>(securityTipCategoryViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
