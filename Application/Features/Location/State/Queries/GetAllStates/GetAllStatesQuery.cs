using Application.Features.Location.State;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location.State
{
    public class GetAllStatesQuery : IRequest<PagedResponse<IEnumerable<GetAllStatesViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQuery, PagedResponse<IEnumerable<GetAllStatesViewModel>>>
    {
        private readonly IStateRepositoryAsync _stateRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllStatesQueryHandler(IStateRepositoryAsync stateRepositoryAsync, IMapper mapper)
        {
            _stateRepositoryAsync = stateRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllStatesViewModel>>> Handle(GetAllStatesQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllStatesParameter>(request);
            var states = await _stateRepositoryAsync.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var statesViewModel = _mapper.Map<IEnumerable<GetAllStatesViewModel>>(states);
            return new PagedResponse<IEnumerable<GetAllStatesViewModel>>(statesViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
