using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
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
    public class GetDistrictByIdQuery : IRequest<Response<GetDistrictViewModel>>
    {
        public int Id { get; set; }
        public class GetDistrictByIdQueryHandler : IRequestHandler<GetDistrictByIdQuery, Response<GetDistrictViewModel>>
        {
            private readonly ITownRepositoryAsync _townRepositoryAsync;
            public GetDistrictByIdQueryHandler(ITownRepositoryAsync townRepositoryAsync)
            {
                _townRepositoryAsync = townRepositoryAsync;
            }

            public async Task<Response<GetDistrictViewModel>> Handle(GetDistrictByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    GetDistrictViewModel getAllDistrictsViewModel = new GetDistrictViewModel();

                    var district = await _townRepositoryAsync.GetTownWithLGAAsync(query.Id);
                    if (district == null)
                        //throw new ApiException($"district not found.");
                        return new Response<GetDistrictViewModel>(getAllDistrictsViewModel, message: $"District not found.", successStatus: false);


                    getAllDistrictsViewModel.DistrictName = district.Name;
                    getAllDistrictsViewModel.Created = district.Created;
                    getAllDistrictsViewModel.LastModified = district.LastModified;
                    getAllDistrictsViewModel.LGA = district.LGA.Name;
                    return new Response<GetDistrictViewModel>(getAllDistrictsViewModel, message: $"District retrieval successful", successStatus: true);
                }
                catch (Exception ex)
                {
                   throw new ApiException($"district not found.");
                }
            }
        }
    }
}
