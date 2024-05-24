using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.LocationEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class UpdateDistrictCommand : IRequest<Response<Town>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LGAId { get; set; }

        public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, Response<Town>>
        {
            private readonly ITownRepositoryAsync _townRepositoryAsync;
            private readonly IUserAccessor _userAccessor;

            public UpdateDistrictCommandHandler(ITownRepositoryAsync townRepositoryAsync, IUserAccessor userAccessor)
            {
                _townRepositoryAsync = townRepositoryAsync;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            }

            public async Task<Response<Town>> Handle(UpdateDistrictCommand command, CancellationToken cancellationToken)
            {
               try
               {
                    string ModifiedBy = _userAccessor.GetUserId();

                    //var trustedPerson = await _trustedPersonRepository.GetByIdAsync(command.UserId);
                    var town = await _townRepositoryAsync.GetByIdAsync(command.Id);

                    if (town == null)
                    {
                        throw new ApiException($"district not found.");
                    }
                    else
                    {
                        town.Name = command.Name;
                        town.LGAId = Guid.Parse(command.LGAId);
                        town.LastModifiedBy = ModifiedBy;
                        town.LastModified = DateTime.UtcNow.AddHours(1);
                        await _townRepositoryAsync.UpdateAsync(town);

                        return new Response<Town>(town, responsestatus: APIResponseStatus.success.ToString(), $"District successfully updated");
                    }
               }
                catch (Exception ex)
               {
                    throw new ApiException($"An error occurred while updating the district: {ex.Message}");
               }
            }
        }
    }
}
