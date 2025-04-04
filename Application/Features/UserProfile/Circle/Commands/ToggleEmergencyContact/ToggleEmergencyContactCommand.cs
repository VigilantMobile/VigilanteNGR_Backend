// Location: Application/Features/UserProfile/Circle/Commands/ToggleEmergencyContact/ToggleEmergencyContactCommand.cs
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class ToggleEmergencyContactCommand : IRequest<Response<bool>>
    {
        public string userId { get; set; }
        public string memberId { get; set; }
        public bool isEmergencyContact { get; set; }
    }

    public class ToggleEmergencyContactCommandHandler : IRequestHandler<ToggleEmergencyContactCommand, Response<bool>>
    {
        private readonly ICustomerService _customerService;

        public ToggleEmergencyContactCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Response<bool>> Handle(ToggleEmergencyContactCommand request, CancellationToken cancellationToken)
        {
            var model = new ToggleEmergencyContactViewModel
            {
                userId = request.userId,
                memberId = request.memberId,
                isEmergencyContact = request.isEmergencyContact
            };

            var result = await _customerService.ToggleEmergencyContactStatus(model);
            return new Response<bool>(result, APIResponseStatus.success.ToString(),
                result ? "Emergency contact status updated successfully." : "Failed to update emergency contact status.");
        }
    }
}
