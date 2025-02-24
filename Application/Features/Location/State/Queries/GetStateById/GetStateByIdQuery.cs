using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class GetStateByIdQuery : IRequest<Response<GetStateViewModel>>
    {
        public string Id { get; set; }
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
                        return new Response<GetStateViewModel>(getstateViewModel, responsestatus: APIResponseStatus.fail.ToString(), message: $"state not found.");

                    getstateViewModel.StateName = state.Name;
                    getstateViewModel.Created = state.Created;
                    getstateViewModel.LastModified = state.LastModified;
                    return new Response<GetStateViewModel>(getstateViewModel, responsestatus: APIResponseStatus.success.ToString(), message: $"state retrieval successful");
                }
                catch (Exception ex)
                {
                   throw new ApiException($"state not found: {ex.Message}");
                }
            }
        }
    }
}
