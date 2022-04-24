using Application.Exceptions;
using Application.Features.UserProfile.Customer.Queries.GetCustomerProfile;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTip;
using Domain.Entities.LocationEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
