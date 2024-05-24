using Application.Exceptions;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class GetDistrictByIdQuery : IRequest<Response<GetDistrictViewModel>>
    {
        public string Id { get; set; }
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
                        return new Response<GetDistrictViewModel>(getAllDistrictsViewModel, responsestatus: APIResponseStatus.fail.ToString(), message: $"District not found.");


                    getAllDistrictsViewModel.DistrictName = district.Name;
                    getAllDistrictsViewModel.Created = district.Created;
                    getAllDistrictsViewModel.LastModified = district.LastModified;
                    getAllDistrictsViewModel.LGA = district.LGA.Name;
                    return new Response<GetDistrictViewModel>(getAllDistrictsViewModel, responsestatus: APIResponseStatus.success.ToString(), message: $"District retrieval successful");
                }
                catch (Exception ex)
                {
                    throw new ApiException($"district not found.");
                }
            }
        }
    }
}
