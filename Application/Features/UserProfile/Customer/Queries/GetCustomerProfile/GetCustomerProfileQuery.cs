using Application.Exceptions;
using Application.Features.UserProfile.Customer.Queries.GetCustomerProfile;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Location
{
    public class GetCustomerProfileQuery : IRequest<Response<CustomerProfileVM>>
    {
        public string CustomerId { get; set; }
        public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, Response<CustomerProfileVM>>
        {
            private readonly ICustomerProfileService _customerProfileService;
            public GetCustomerProfileQueryHandler(ICustomerProfileService customerProfileService)
            {
                _customerProfileService = customerProfileService;
            }

            public async Task<Response<CustomerProfileVM>> Handle(GetCustomerProfileQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var customerProfile = await _customerProfileService.GetCustomerProfileAsync(query.CustomerId);


                    if (customerProfile == null)
                        throw new ApiException($"Unable to retrieve customer profile.");

                    return new Response<CustomerProfileVM>(customerProfile, message: $"Customer profile successfully retrieved.", successStatus: true);
                }
                catch (Exception ex)
                {
                    throw new ApiException($"Unable to retrieve customer profile: {ex.Message}");
                }
            }
        }
    }
}
