using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class DeleteTrustedPersonByIdCommand : IRequest<Response<UserCircle>>
    {
        [Required]
        public string TrustedPersonId { get; set; }
        public class DeleteTrustedPersonByIdCommandHandler : IRequestHandler<DeleteTrustedPersonByIdCommand, Response<UserCircle>>
        {
            private readonly ITrustedPersonRepositoryAsync _trustedPersonRepository;
            private readonly IUserAccessor _userAccessor;

            public DeleteTrustedPersonByIdCommandHandler(ITrustedPersonRepositoryAsync trustedPersonRepository, IUserAccessor userAccessor)
            {
                _trustedPersonRepository = trustedPersonRepository;
                _userAccessor = userAccessor;
            }
            public async Task<Response<UserCircle>> Handle(DeleteTrustedPersonByIdCommand command, CancellationToken cancellationToken)
            {
                string OwnerId = _userAccessor.GetUserId();

                var trustedPerson = await _trustedPersonRepository.IsOwnedByOwner(command.TrustedPersonId, OwnerId);

                if (trustedPerson == null)
                {
                    throw new ApiException($"Trusted contact not found.");
                }
                else
                {
                    await _trustedPersonRepository.DeleteAsync(trustedPerson);

                    //return new Response<Product>(product.Id);
                    return new Response<UserCircle>(null, responsestatus: APIResponseStatus.success.ToString(), $"Trusted contact was successfully deleted.");
                }
            }
        }
    }
}
