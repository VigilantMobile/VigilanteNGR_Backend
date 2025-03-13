using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public partial class ToggleMemberVisibilityCommand : IRequest<Response<bool>>
    {
        public ToggleCircleMembershipVisibilityViewModel model { get; set; }
    }

    public class ToggleProfileVisibilityCommandHandler : IRequestHandler<ToggleMemberVisibilityCommand, Response<bool>>
    {
        private readonly ICustomerService _customerService;

        public ToggleProfileVisibilityCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Response<bool>> Handle(ToggleMemberVisibilityCommand request, CancellationToken cancellationToken)
        {
            bool result = await _customerService.ToggleCustomerProfileVisibility(request.model);
            string message = request.model.isProfileVisible
                ? "Profile visibility turned on successfully."
                : "Profile visibility turned off successfully.";

            return new Response<bool>(result, message);
        }
    }
}
