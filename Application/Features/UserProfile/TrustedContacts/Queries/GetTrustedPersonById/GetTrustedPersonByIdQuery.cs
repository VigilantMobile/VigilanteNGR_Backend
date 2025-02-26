using Application.Exceptions;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class GetTrustedPersonByIdQuery : IRequest<Response<TrustedPerson>>
    {
        public string Id { get; set; }
        public class GetTrustedPersonByIdQueryHandler : IRequestHandler<GetTrustedPersonByIdQuery, Response<TrustedPerson>>
        {
            private readonly ITrustedPersonRepositoryAsync _trustedPersonRepository;
            public GetTrustedPersonByIdQueryHandler(ITrustedPersonRepositoryAsync trustedPersonRepository)
            {
                _trustedPersonRepository = trustedPersonRepository;
            }
            public async Task<Response<TrustedPerson>> Handle(GetTrustedPersonByIdQuery query, CancellationToken cancellationToken)
            {
                var trustedPerson = await _trustedPersonRepository.GetByIdAsync(query.Id);
                if (trustedPerson == null)

                    throw new ApiException($"Trusted contact not found.");

                return new Response<TrustedPerson>(trustedPerson, responsestatus: APIResponseStatus.success.ToString(), message: $"Trusted contact successfully retrieved.");

            }
        }
    }
}
