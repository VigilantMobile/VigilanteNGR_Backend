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
    public class DeactivateFriendshipCommand : IRequest<DataListResponse<bool>>
    {
        public string CustomerId { get; set; }
        public string FriendId { get; set; }
    }

    public class DeactivateFriendshipCommandHandler : IRequestHandler<DeactivateFriendshipCommand, DataListResponse<bool>>
    {
        private readonly ICustomerService _customerService;

        public DeactivateFriendshipCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<DataListResponse<bool>> Handle(DeactivateFriendshipCommand request, CancellationToken cancellationToken)
        {
            var result = await _customerService.DeactivateFriendship(request.CustomerId, request.FriendId);
            return new DataListResponse<bool>(result, "Friendship successfully deactivated.", successStatus: true);
        }
    }
}
