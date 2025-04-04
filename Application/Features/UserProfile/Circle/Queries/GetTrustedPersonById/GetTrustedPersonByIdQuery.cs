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
    public class GetTrustedPersonByIdQuery : IRequest<Response<UserCircle>>
    {
        public string Id { get; set; }
        public class GetTrustedPersonByIdQueryHandler : IRequestHandler<GetTrustedPersonByIdQuery, Response<UserCircle>>
        {
            private readonly ICircleRepositoryAsync _trustedPersonRepository;
            public GetTrustedPersonByIdQueryHandler(ICircleRepositoryAsync trustedPersonRepository)
            {
                _trustedPersonRepository = trustedPersonRepository;
            }
            public async Task<Response<UserCircle>> Handle(GetTrustedPersonByIdQuery query, CancellationToken cancellationToken)
            {
                var trustedPerson = await _trustedPersonRepository.GetByIdAsync(query.Id);
                if (trustedPerson == null)

                    throw new ApiException($"Trusted contact not found.");

                return new Response<UserCircle>(trustedPerson, responsestatus: APIResponseStatus.success.ToString(), message: $"Trusted contact successfully retrieved.");

            }
        }
    }
}
