using Application.Interfaces;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class RejectTrustedContactInvitationCommand : IRequest<DataListResponse<bool>>
    {
        public string CustomerId { get; set; }
        public string InviterId { get; set; }
    }

    public class RejectTrustedContactInvitationCommandHandler : IRequestHandler<RejectTrustedContactInvitationCommand, DataListResponse<bool>>
    {
        private readonly ICustomerService _customerService;
        private readonly IUserAccessor _userAccessor;

        public RejectTrustedContactInvitationCommandHandler(ICustomerService customerService, IUserAccessor userAccessor)
        {
            _customerService = customerService;
            _userAccessor = userAccessor;
        }

        public async Task<DataListResponse<bool>> Handle(RejectTrustedContactInvitationCommand request, CancellationToken cancellationToken)
        {
            if (request.CustomerId == request.InviterId)
            {
                return new DataListResponse<bool>(false, "You cannot add yourself as a trusted contact.", successStatus: false);
            }
            else if (request.CustomerId != _userAccessor.GetUserId())
            {
                return new DataListResponse<bool>(false, "Invalid Token or CustomerId", successStatus: false);

            }

            var viewModel = new RejectCircleInvitationViewModel
            {
                userId = request.CustomerId,
                inviterId = request.InviterId
            };

            var rejectInviteStatus = await _customerService.RejectCircleInvitation(viewModel);
            return new DataListResponse<bool>(rejectInviteStatus, "Trusted contact invitation successfully rejected.", successStatus: true);
        }
    }
}
