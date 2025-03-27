// Location: Application/Features/UserProfile/Circle/Queries/GetEmergencyContacts/GetEmergencyContactsQuery.cs
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class GetEmergencyContactsQuery : IRequest<Response<List<EmergencyContactViewModel>>>
    {
        public string userId { get; set; }
    }

    public class GetEmergencyContactsQueryHandler : IRequestHandler<GetEmergencyContactsQuery, Response<List<EmergencyContactViewModel>>>
    {
        private readonly ICustomerService _customerService;

        public GetEmergencyContactsQueryHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Response<List<EmergencyContactViewModel>>> Handle(GetEmergencyContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _customerService.GetUserEmergencyContacts(request.userId);

            return new Response<List<EmergencyContactViewModel>>(
                contacts,
                APIResponseStatus.success.ToString(),
                contacts.Count > 0 ? $"Found {contacts.Count} emergency contacts." : "No emergency contacts found.");
        }
    }
}
