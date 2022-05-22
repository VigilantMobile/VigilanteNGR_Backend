using Application.Exceptions;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class GetStateByIdQuery : IRequest<Response<GetStateViewModel>>
    {
        public int Id { get; set; }
        public class GetLGAByIdQueryHandler : IRequestHandler<GetStateByIdQuery, Response<GetStateViewModel>>
        {
            private readonly IStateRepositoryAsync _stateRepositoryAsync;
            public GetLGAByIdQueryHandler(IStateRepositoryAsync stateRepositoryAsync)
            {
                _stateRepositoryAsync = stateRepositoryAsync;
            }

            public async Task<Response<GetStateViewModel>> Handle(GetStateByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    GetStateViewModel getstateViewModel = new GetStateViewModel();

                    var state = await _stateRepositoryAsync.GetByIdAsync(query.Id);
                    if (state == null)
                        //throw new ApiException($"district not found.");
                        return new Response<GetStateViewModel>(getstateViewModel, message: $"state not found.", successStatus: false);

                    getstateViewModel.StateName = state.Name;
                    getstateViewModel.Created = state.Created;
                    getstateViewModel.LastModified = state.LastModified;
                    return new Response<GetStateViewModel>(getstateViewModel, message: $"state retrieval successful", successStatus: true);
                }
                catch (Exception ex)
                {
                    throw new ApiException($"state not found: {ex.Message}");
                }
            }
        }
    }
}
