using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppTroopers.Panic.Commands.CreateTrustedPerson
{
    public partial class CreateTrustedPersonCommand : IRequest<Response<string>>
    {
        public List<TrustedPerson> trustedPeople { get; set; }
    }
    public class CreateTrustedPersonCommandHandler : IRequestHandler<CreateTrustedPersonCommand, Response<string>>
    {
        private readonly ITrustedPersonRepositoryAsync _trustedPersonRepository;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateTrustedPersonCommandHandler(ITrustedPersonRepositoryAsync trustedPersonRepository, IMapper mapper, IUserAccessor userAccessor)
        {
            _trustedPersonRepository = trustedPersonRepository;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<string>> Handle(CreateTrustedPersonCommand request, CancellationToken cancellationToken)
        {
            string OwnerId = _userAccessor.GetUserId();
            foreach (var trustedContact in request.trustedPeople)
            {
                var contact = _mapper.Map<TrustedPerson>(trustedContact);
                contact.OwnerId = OwnerId;
                contact.CreatedBy = OwnerId;
                contact.Created = DateTime.UtcNow.AddHours(1);
                await _trustedPersonRepository.AddAsync(contact, _userAccessor.GetUserId());
            }

            return new Response<string>(null, $"Trusted contact(s) successfully created.", successStatus: true);
        }
    }
}
