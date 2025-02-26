using Application.Filters;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class GetAllTrustedPeopleQuery : IRequest<PagedResponse<IEnumerable<GetAllTrustedPeopleViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllTrustedPeopleQueryHandler : IRequestHandler<GetAllTrustedPeopleQuery, PagedResponse<IEnumerable<GetAllTrustedPeopleViewModel>>>
    {
        private readonly ITrustedPersonRepositoryAsync _trustedPersonRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllTrustedPeopleQueryHandler(ITrustedPersonRepositoryAsync trustedPersonRepositoryAsync, IMapper mapper)
        {
            _trustedPersonRepositoryAsync = trustedPersonRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllTrustedPeopleViewModel>>> Handle(GetAllTrustedPeopleQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTrustedPeopleParameter>(request);
            var TrustedPeople = await _trustedPersonRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var trustedPeopleViewModel = _mapper.Map<IEnumerable<GetAllTrustedPeopleViewModel>>(TrustedPeople);
            return new PagedResponse<IEnumerable<GetAllTrustedPeopleViewModel>>(trustedPeopleViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
