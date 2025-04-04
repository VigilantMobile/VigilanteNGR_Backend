using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{

    public class ReactivateFriendshipCommand : IRequest<DataListResponse<bool>>
    {
        public string userId { get; set; }
        public string memberId { get; set; }
    }

    public class ReactivateFriendshipCommandHandler : IRequestHandler<ReactivateFriendshipCommand, DataListResponse<bool>>
    {
        private readonly ICustomerService _customerService;

        public ReactivateFriendshipCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<DataListResponse<bool>> Handle(ReactivateFriendshipCommand request, CancellationToken cancellationToken)
        {
            var model = new ReactivateCircleMembershipViewModel
            {
                userId = request.userId,
                memberId = request.memberId
            };
            var result = await _customerService.ReactivateFriendship(model);
            return new DataListResponse<bool>(result, "Friendship reactivation request initiated successfully.", successStatus: true);
        }
    }
}
