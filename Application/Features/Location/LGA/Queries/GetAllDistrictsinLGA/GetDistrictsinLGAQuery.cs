using Application.Exceptions;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class GetDistrictsinLGAQuery : IRequest<Response<GetAllDistrictsinLGAViewModel>>
    {
        public string LGAId { get; set; }
        public class GetDistrictinLGAQueryHandler : IRequestHandler<GetDistrictsinLGAQuery, Response<GetAllDistrictsinLGAViewModel>>
        {
            private readonly ILGARepositoryAsync lgaRepositoryAsync;
            public GetDistrictinLGAQueryHandler(ILGARepositoryAsync lgaRepositoryAsync)
            {
                this.lgaRepositoryAsync = lgaRepositoryAsync;
            }

            public async Task<Response<GetAllDistrictsinLGAViewModel>> Handle(GetDistrictsinLGAQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    List<DistrictViewModel> districts = new List<DistrictViewModel>();

                    var lga = await lgaRepositoryAsync.GetByIdAsync(query.LGAId);
                    var districtsinLGA = await lgaRepositoryAsync.GetDistrictsinLGAAsync(query.LGAId);
                    if (districtsinLGA == null)
                    {
                        return new Response<GetAllDistrictsinLGAViewModel>(new GetAllDistrictsinLGAViewModel { }, responsestatus: ResponseStatus.fail.ToString(), message: $"No districts found in the specified LGA");
                    }

                    foreach (var district in districtsinLGA)
                    {
                        DistrictViewModel districtViewModel = new DistrictViewModel
                        {
                            Id = district.Id.ToString(),
                            DistrictName = district.Name,
                            Created = district.Created,
                        };
                        districts.Add(districtViewModel);
                    }

                    GetAllDistrictsinLGAViewModel getAllDistrictsViewModel = new GetAllDistrictsinLGAViewModel
                    {
                        LGA = lga.Name,
                        LGAId = lga.Id.ToString(),
                        Districts = districts,
                        Count = districts.Count
                    };

                    return new Response<GetAllDistrictsinLGAViewModel>(getAllDistrictsViewModel, responsestatus: ResponseStatus.success.ToString(), message: $"Successfully retrieved districts for the specified LGA.");
                }
                catch (Exception ex)
                {
                    throw new ApiException($"district not found.");
                }
            }
        }
    }
}
