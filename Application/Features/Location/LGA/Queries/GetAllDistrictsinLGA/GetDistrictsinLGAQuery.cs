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
    public class GetDistrictinLGAQuery : IRequest<Response<GetAllDistrictsinLGAViewModel>>
    {
        public int LGAId { get; set; }
        public class GetDistrictByIdQueryHandler : IRequestHandler<GetDistrictinLGAQuery, Response<GetAllDistrictsinLGAViewModel>>
        {
            private readonly ILGARepositoryAsync lgaRepositoryAsync;
            public GetDistrictByIdQueryHandler(ILGARepositoryAsync lgaRepositoryAsync)
            {
                this.lgaRepositoryAsync = lgaRepositoryAsync;
            }

            public async Task<Response<GetAllDistrictsinLGAViewModel>> Handle(GetDistrictinLGAQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    List<DistrictViewModel> districts = new List<DistrictViewModel>();

                    var lga = await lgaRepositoryAsync.GetByIdAsync(query.LGAId);
                    var districtsinLGA = await lgaRepositoryAsync.GetDistrictsinLGAAsync(query.LGAId);
                    if (districtsinLGA == null)
                    {
                        return new Response<GetAllDistrictsinLGAViewModel>(new GetAllDistrictsinLGAViewModel { }, message: $"No districts found in the specified LGA", successStatus: false);
                    }

                    foreach (var district in districtsinLGA)
                    {
                        DistrictViewModel districtViewModel = new DistrictViewModel
                        {
                            Id = district.Id,
                            DistrictName = district.Name,
                            Created = district.Created,
                        };
                        districts.Add(districtViewModel);
                    }

                    GetAllDistrictsinLGAViewModel getAllDistrictsViewModel = new GetAllDistrictsinLGAViewModel
                    {
                        LGA = lga.Name,
                        LGAId = lga.Id,
                        Districts = districts,
                        Count = districts.Count
                    };

                    return new Response<GetAllDistrictsinLGAViewModel>(getAllDistrictsViewModel, message: $"District retrieval successful", successStatus: true);
                }
                catch (Exception ex)
                {
                   throw new ApiException($"district not found.");
                }
            }
        }
    }
}
