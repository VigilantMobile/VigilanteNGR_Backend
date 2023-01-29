using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.Panic.Commands.DeleteTrustedPersonById
{
    public class DeleteTrustedPersonByIdCommand : IRequest<Response<TrustedPerson>>
    {
        public string Id { get; set; }
        public class DeleteTrustedPersonByIdCommandHandler : IRequestHandler<DeleteTrustedPersonByIdCommand, Response<TrustedPerson>>
        {
            private readonly ITrustedPersonRepositoryAsync _trustedPersonRepository;
            private readonly IUserAccessor _userAccessor;

            public DeleteTrustedPersonByIdCommandHandler(ITrustedPersonRepositoryAsync trustedPersonRepository, IUserAccessor userAccessor)
            {
                _trustedPersonRepository = trustedPersonRepository;
                _userAccessor = userAccessor;
            }
            public async Task<Response<TrustedPerson>> Handle(DeleteTrustedPersonByIdCommand command, CancellationToken cancellationToken)
            {
                string OwnerId = _userAccessor.GetUserId();

                var trustedPerson = await _trustedPersonRepository.IsOwnedByOwner(command.Id, OwnerId);

                if (trustedPerson == null)
                {
                    throw new ApiException($"Trusted contact not found.");
                }
                else
                {
                    await _trustedPersonRepository.DeleteAsync(trustedPerson);

                    //return new Response<Product>(product.Id);
                    return new Response<TrustedPerson>(null, $"Trusted contact was successfully deleted.", successStatus: true);
                }
            }
        }
    }
}
