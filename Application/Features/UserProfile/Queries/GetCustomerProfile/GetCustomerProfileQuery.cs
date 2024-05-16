using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Location;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using Domain.Common.Enums;
using Domain.Entities;
using Domain.Entities.AppTroopers.Panic;
using Domain.Entities.AppTroopers.SecurityTips;
using Domain.Entities.LocationEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class GetCustomerProfileQuery : IRequest<Response<CustomerProfileViewModel>>
    {
        public string CustomerId { get; set; }
        public class GetCustomerProfileQueryHandler : IRequestHandler<GetCustomerProfileQuery, Response<CustomerProfileViewModel>>
        {
            private readonly ICustomerService _customerProfileService;
            public GetCustomerProfileQueryHandler(ICustomerService customerProfileService)
            {
                _customerProfileService = customerProfileService;
            }

            public async Task<Response<CustomerProfileViewModel>> Handle(GetCustomerProfileQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var customerProfile = await _customerProfileService.GetCustomerProfileAsync(query.CustomerId);


                    if (customerProfile == null)
                        throw new ApiException($"Unable to retrieve customer profile.");

                    return new Response<CustomerProfileViewModel>(customerProfile, responsestatus: ResponseStatus.success.ToString(), message: $"Customer profile successfully retrieved.");
                }
                catch (Exception ex)
                {
                    throw new ApiException($"Unable to retrieve customer profile: {ex.Message}");
                }
            }
        }
    }
}
