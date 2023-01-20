using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.Panic.Commands.UpdateTrustedPerson
{
    public class UpdateTrustedPersonCommand : IRequest<Response<TrustedPerson>>
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }

        public class UpdateTrustedPersonCommandHandler : IRequestHandler<UpdateTrustedPersonCommand, Response<TrustedPerson>>
        {
            private readonly ITrustedPersonRepositoryAsync _trustedPersonRepository;
            private readonly IUserAccessor _userAccessor;

            public UpdateTrustedPersonCommandHandler(ITrustedPersonRepositoryAsync trustedPersonRepository, IUserAccessor userAccessor)
            {
                _trustedPersonRepository = trustedPersonRepository;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            }

            public async Task<Response<TrustedPerson>> Handle(UpdateTrustedPersonCommand command, CancellationToken cancellationToken)
            {
                string OwnerId = _userAccessor.GetUserId();

                //var trustedPerson = await _trustedPersonRepository.GetByIdAsync(command.UserId);
                var trustedPerson = await _trustedPersonRepository.IsOwnedByOwner(command.UserId, OwnerId);

                if (trustedPerson == null)
                {
                    throw new ApiException($"Trusted contact not found.");
                }
                else
                {
                    trustedPerson.FullName = command.FullName;
                    trustedPerson.PhoneNumber = command.PhoneNumber;
                    trustedPerson.EmailAddress = command.EmailAddress;
                    trustedPerson.LastModified = DateTime.UtcNow.AddHours(1);
                    await _trustedPersonRepository.UpdateAsync(trustedPerson);

                    return new Response<TrustedPerson>(trustedPerson, $"Trusted contact successfully updated", successStatus: true);
                }
            }
        }
    }
}
