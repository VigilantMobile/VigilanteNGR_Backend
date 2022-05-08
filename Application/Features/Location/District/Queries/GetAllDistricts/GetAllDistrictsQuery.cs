using Application.Features.AppTroopers.SecurityTips.Queries.GetAllSecurityTipCategories;
using Application.Filters;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class GetAllDistrictsQuery : IRequest<PagedResponse<IEnumerable<GetAllDistrictsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllDistrictsQueryHandler : IRequestHandler<GetAllDistrictsQuery, PagedResponse<IEnumerable<GetAllDistrictsViewModel>>>
    {
        private readonly ITownRepositoryAsync _townRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllDistrictsQueryHandler(ITownRepositoryAsync townRepositoryAsync, IMapper mapper)
        {
            _townRepositoryAsync = townRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllDistrictsViewModel>>> Handle(GetAllDistrictsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllDistrictsParameter>(request);
            var districts = await _townRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber,validFilter.PageSize);
            var districtsViewModel = _mapper.Map<IEnumerable<GetAllDistrictsViewModel>>(districts);
            return new PagedResponse<IEnumerable<GetAllDistrictsViewModel>>(districtsViewModel, validFilter.PageNumber, validFilter.PageSize);           
        }
    }
}
