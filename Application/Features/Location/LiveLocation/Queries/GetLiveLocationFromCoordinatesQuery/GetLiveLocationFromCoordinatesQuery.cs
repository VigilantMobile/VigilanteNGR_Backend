using Application.Exceptions;
using Application.Features.Location.LiveLocation;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Services.Interfaces.Location;
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
    public class GetLiveLocationFromCoordinatesQuery : IRequest<Response<LiveLocationViewModel>>
    {
        public string Coordinates { get; set; }
        public class GetLiveLocationFromCoordinatesQueryHandler : IRequestHandler<GetLiveLocationFromCoordinatesQuery, Response<LiveLocationViewModel>>
        {
            private readonly IGeoCodingService _geoCodingService;
            public GetLiveLocationFromCoordinatesQueryHandler(IGeoCodingService geoCodingService)
            {
                _geoCodingService = geoCodingService;
            }

            public async Task<Response<LiveLocationViewModel>> Handle(GetLiveLocationFromCoordinatesQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    LiveLocationViewModel liveLocation = new LiveLocationViewModel();

                    var location = await _geoCodingService.GetCustomerLiveAddresses(query.Coordinates);
                    if (location == null)
                        //throw new ApiException($"district not found.");
                        return new Response<LiveLocationViewModel>(liveLocation, responsestatus: APIResponseStatus.fail.ToString(), message: $"live location not found.");

                    liveLocation.CountryName = location.Data.Country;
                    liveLocation.StateName = location.Data.StateOrProvinceOrRegion;
                    liveLocation.CountyName = location.Data.CountryOrDistrictOrLGA;
                    liveLocation.DistrictName = location.Data.TownOrDistrict;
                    liveLocation.FormattedAddress = location.Data.FormattedAddress;
                    return new Response<LiveLocationViewModel>(liveLocation, responsestatus: APIResponseStatus.success.ToString(), message: $"{location.Message}");
                }
                catch (Exception ex)
                {
                   throw new ApiException($"location not found: {ex.Message}");
                }
            }
        }
    }
}
