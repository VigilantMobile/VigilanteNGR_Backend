using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.Location;
using Application.Wrappers;
using Domain.Entities.LocationEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class UpdateTownCommand : IRequest<Response<Town>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LGAId { get; set; }

        public class UpdateTownCommandHandler : IRequestHandler<UpdateTownCommand, Response<Town>>
        {
            private readonly ITownRepositoryAsync _townRepositoryAsync;
            private readonly IUserAccessor _userAccessor;

            public UpdateTownCommandHandler(ITownRepositoryAsync townRepositoryAsync, IUserAccessor userAccessor)
            {
                _townRepositoryAsync = townRepositoryAsync;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            }

            public async Task<Response<Town>> Handle(UpdateTownCommand command, CancellationToken cancellationToken)
            {
                string CreatedBy = _userAccessor.GetUserId();

                //var trustedPerson = await _trustedPersonRepository.GetByIdAsync(command.UserId);
                var town = await _townRepositoryAsync.GetByIdAsync(command.Id);

                if (town == null)
                {
                    throw new ApiException($"district not found.");
                }
                else
                {
                    town.Name = command.Name;
                    town.LGAId = command.LGAId;
                    town.Created = DateTime.UtcNow.AddHours(1);
                    town.CreatedBy = CreatedBy;
                    await _townRepositoryAsync.UpdateAsync(town);

                    return new Response<Town>(town, $"District successfully updated", successStatus: true);
                }
            }
        }
    }
}
