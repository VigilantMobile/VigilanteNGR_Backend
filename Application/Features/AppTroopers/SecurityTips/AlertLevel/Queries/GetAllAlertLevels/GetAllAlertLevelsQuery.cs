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
    public class GetAllAlertLevelsQuery : IRequest<PagedResponse<IEnumerable<GetAllAlertLevelsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllAlertLevelsQueryHandler : IRequestHandler<GetAllAlertLevelsQuery, PagedResponse<IEnumerable<GetAllAlertLevelsViewModel>>>
    {
        private readonly IAlertLevelRepositoryAsync _alertLevelRepository;
        private readonly IMapper _mapper;
        public GetAllAlertLevelsQueryHandler(IAlertLevelRepositoryAsync alertLevelsRepository, IMapper mapper)
        {
            _alertLevelRepository =  alertLevelsRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllAlertLevelsViewModel>>> Handle(GetAllAlertLevelsQuery request, CancellationToken cancellationToken)
        {
            //var validFilter = _mapper.Map<GetAllSecurityTipCategoriesQueryParameter>(request);
            var alertLevels = await _alertLevelRepository.GetPagedReponseAsync(request.PageNumber, request.PageSize);
            var securityTipCategoryViewModel = _mapper.Map<IEnumerable<GetAllAlertLevelsViewModel>>(alertLevels);
            return new PagedResponse<IEnumerable<GetAllAlertLevelsViewModel>>(securityTipCategoryViewModel, request.PageNumber, request.PageSize);
        }
    }
}
