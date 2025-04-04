using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class UpdateTrustedPersonCommand : IRequest<Response<UserCircle>>
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string TrustedPersonId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Gender { get; set; }

        public class UpdateTrustedPersonCommandHandler : IRequestHandler<UpdateTrustedPersonCommand, Response<UserCircle>>
        {
            private readonly ICircleRepositoryAsync _trustedPersonRepository;
            private readonly IUserAccessor _userAccessor;

            public UpdateTrustedPersonCommandHandler(ICircleRepositoryAsync trustedPersonRepository, IUserAccessor userAccessor)
            {
                _trustedPersonRepository = trustedPersonRepository;
                _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
            }

            public async Task<Response<UserCircle>> Handle(UpdateTrustedPersonCommand command, CancellationToken cancellationToken)
            {
                string InviterId = _userAccessor.GetUserId();

                //var trustedPerson = await _trustedPersonRepository.GetByIdAsync(command.UserId);
                var trustedPerson = await _trustedPersonRepository.IsOwnedByOwner(command.TrustedPersonId, InviterId);

                if (trustedPerson == null)
                {
                    throw new ApiException($"Trusted contact not found.");
                }
                else
                {
                    trustedPerson.FullName = command.FullName;
                    trustedPerson.FullAddress = command.FullName;
                    trustedPerson.PhoneNumber = command.PhoneNumber;
                    trustedPerson.EmailAddress = command.EmailAddress;
                    trustedPerson.LastModified = DateTime.UtcNow.AddHours(1);

                    await _trustedPersonRepository.UpdateAsync(trustedPerson);

                    return new Response<UserCircle>(trustedPerson, responsestatus: APIResponseStatus.success.ToString(), $"Trusted contact successfully updated");
                }
            }
        }
    }
}
