using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class AcceptTrustedContactInvitationCommand : IRequest<DataListResponse<bool>>
    {
        public string CustomerId { get; set; }
        public string InviterId { get; set; }
    }

    public class AcceptTrustedContactInvitationCommandHandler : IRequestHandler<AcceptTrustedContactInvitationCommand, DataListResponse<bool>>
    {
        private readonly ICustomerService _customerService;

        public AcceptTrustedContactInvitationCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<DataListResponse<bool>> Handle(AcceptTrustedContactInvitationCommand request, CancellationToken cancellationToken)
        {
            var viewModel = new AcceptCustomerTrustedContactInvitationViewModel
            {
                CustomerId = request.CustomerId,
                InviterId = request.InviterId
            };

           var acceptInviteStatus = await _customerService.AcceptCircleInvitation(viewModel);
            return new DataListResponse<bool>(acceptInviteStatus, $"Trusted contact(s) successfully created.", successStatus: true);
        }
    }
}
