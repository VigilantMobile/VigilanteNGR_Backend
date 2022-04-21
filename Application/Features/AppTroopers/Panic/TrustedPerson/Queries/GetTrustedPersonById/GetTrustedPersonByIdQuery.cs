using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.Panic.GetTrustedPersonById
{
    public class GetTrustedPersonByIdQuery : IRequest<Response<TrustedPerson>>
    {
        public int Id { get; set; }
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

                return new Response<TrustedPerson>(trustedPerson, message: $"Trusted contact successfully retrieved.", successStatus: true);

            }
        }
    }
}
