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
    public class GetLGAByIdQuery : IRequest<Response<GetLGAViewModel>>
    {
        public string LGAId { get; set; }
        public class GetLGAByIdQueryHandler : IRequestHandler<GetLGAByIdQuery, Response<GetLGAViewModel>>
        {
            private readonly ILGARepositoryAsync _lgaRepositoryAsync;
            public GetLGAByIdQueryHandler(ILGARepositoryAsync lgaRepositoryAsync)
            {
                _lgaRepositoryAsync = lgaRepositoryAsync;
            }

            public async Task<Response<GetLGAViewModel>> Handle(GetLGAByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    GetLGAViewModel getlgaViewModel = new GetLGAViewModel();

                    var lga = await _lgaRepositoryAsync.GetLGAWithStateAsync(query.LGAId);
                    if (lga == null)
                        throw new ApiException($"lga not found.");

                    getlgaViewModel.LGAName = lga.Name;
                    getlgaViewModel.Created = lga.Created;
                    getlgaViewModel.LastModified = lga.LastModified;
                    getlgaViewModel.State = lga.State.Name;
                    return new Response<GetLGAViewModel>(getlgaViewModel, responsestatus: ResponseStatus.success.ToString(), message: $"lga retrieval successful");
                }
                catch (Exception ex)
                {
                    throw new ApiException($"lga not found.");
                }
            }
        }
    }
}
