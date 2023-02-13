using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class UpdateTrustedPersonCommand : IRequest<Response<TrustedPerson>>
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string TrustedPersonId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string TownId { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
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
                var trustedPerson = await _trustedPersonRepository.IsOwnedByOwner(command.TrustedPersonId, OwnerId);

                if (trustedPerson == null)
                {
                    throw new ApiException($"Trusted contact not found.");
                }
                else
                {
                    trustedPerson.TownId = Guid.Parse(command.TownId); 
                    trustedPerson.FullName = command.FullName;
                    trustedPerson.FullAddress = command.FullName;
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
