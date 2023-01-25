using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
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
    public class GetLGAsinStateQuery : IRequest<Response<GetAllLGAsinStateViewModel>>
    {
        public string StateId { get; set; }
        public class GetLGAsinStateQueryHandler : IRequestHandler<GetLGAsinStateQuery, Response<GetAllLGAsinStateViewModel>>
        {
            private readonly IStateRepositoryAsync stateRepositoryAsync;
            public GetLGAsinStateQueryHandler(IStateRepositoryAsync stateRepositoryAsync)
            {
                this.stateRepositoryAsync = stateRepositoryAsync;
            }

            public async Task<Response<GetAllLGAsinStateViewModel>> Handle(GetLGAsinStateQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    List<LGAViewModel> lgas = new List<LGAViewModel>();

                    var state = await stateRepositoryAsync.GetByIdAsync(query.StateId);
                    var lgasInState = await stateRepositoryAsync.GetLGAsinStateAsync(query.StateId);
                    if (lgasInState == null)
                    {
                        return new Response<GetAllLGAsinStateViewModel>(new GetAllLGAsinStateViewModel { }, message: $"No lgas found in the specified State.", successStatus: false);
                    }

                    foreach (var lga in lgasInState)
                    {
                        LGAViewModel lgaViewModel = new LGAViewModel
                        {
                            Id = lga.Id.ToString(),
                            LGAName = lga.Name,
                            Created = lga.Created,
                        };
                        lgas.Add(lgaViewModel);
                    }

                    GetAllLGAsinStateViewModel getAllDistrictsViewModel = new GetAllLGAsinStateViewModel
                    {
                        State = state.Name,
                        StateId = state.Id.ToString(),
                        LGAs = lgas,
                        Count = lgas.Count
                    };

                    return new Response<GetAllLGAsinStateViewModel>(getAllDistrictsViewModel, message: $"Successfully retrieved LGAs for the specified state.", successStatus: true);
                }
                catch (Exception ex)
                {
                   throw new ApiException($"AN error occurred while fetching LGAs: {ex.Message}");
                }
            }
        }
    }
}
