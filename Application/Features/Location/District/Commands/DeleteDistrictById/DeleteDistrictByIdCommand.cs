using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.LocationEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class DeleteDistrictByIdCommand : IRequest<Response<Town>>
    {
        public string TownId { get; set; }
        public class DeleteDistrictByIdCommandHandler : IRequestHandler<DeleteDistrictByIdCommand, Response<Town>>
        {
            private readonly ITownRepositoryAsync _townRepositoryAsync;
            private readonly IUserAccessor _userAccessor;

            public DeleteDistrictByIdCommandHandler(ITownRepositoryAsync townRepositoryAsync, IUserAccessor userAccessor)
            {
                _townRepositoryAsync = townRepositoryAsync;
                _userAccessor = userAccessor;
            }
            public async Task<Response<Town>> Handle(DeleteDistrictByIdCommand command, CancellationToken cancellationToken)
            {
                string UpdatedBy = _userAccessor.GetUserId();

                var town = await _townRepositoryAsync.GetByIdAsync(command.TownId);

                if (town == null)
                {
                    throw new ApiException($"Town not found.");
                }
                else
                {
                    await _townRepositoryAsync.DeleteAsync(town);

                    return new Response<Town>(town, responsestatus: APIResponseStatus.success.ToString(), $"Town {town.Name} successfully deleted");

                }
            }
        }
    }
}
