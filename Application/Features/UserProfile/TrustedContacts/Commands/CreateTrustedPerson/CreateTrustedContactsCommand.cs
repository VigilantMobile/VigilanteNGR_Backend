using Application.Features.UserProfile;
using Application.Interfaces;
using Application.Interfaces.Repositories.AppTroopers.Panic;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.AppTroopers.Panic;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public partial class CreateTrustedContactsCommand : IRequest<DataListResponse<List<CircleMemberViewModel>>>
    {
        [Required]
        public CreateCircleMemberViewModel createCustomerTrustedContactsRequest { get; set; }
    }
    public class CreateTrustedPersonCommandHandler : IRequestHandler<CreateTrustedContactsCommand, DataListResponse<List<CircleMemberViewModel>>>
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateTrustedPersonCommandHandler(ICustomerService customerService, IMapper mapper, IUserAccessor userAccessor)
        {
            _customerService = customerService;
            _mapper = mapper;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<DataListResponse<List<CircleMemberViewModel>>>Handle(CreateTrustedContactsCommand request, CancellationToken cancellationToken)
        {
            string InviterId = _userAccessor.GetUserId();
            //foreach (var trustedContact in request.createCustomerTrustedContactsRequest.customerTrustedContacts)
            //{
            //    var contact = _mapper.Map<TrustedPerson>(trustedContact);
            //    contact.OwnerId = OwnerId;
            //    contact.CreatedBy = OwnerId;
            //    contact.Created = DateTime.UtcNow.AddHours(1);
            //    await _trustedPersonRepository.AddAsync(contact, _userAccessor.GetUserId());
            //}

            List<CircleMemberViewModel> createdTrustedContacts = await _customerService.CreateCustomerTrustedContactsAsync(request.createCustomerTrustedContactsRequest);

            return new DataListResponse<List<CircleMemberViewModel>>(createdTrustedContacts, $"Trusted contact(s) successfully created.", successStatus: true);

        }
    }
}
