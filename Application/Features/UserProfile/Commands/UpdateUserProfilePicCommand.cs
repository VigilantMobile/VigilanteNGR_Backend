using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public partial class UpdateCustomerProfileUrlCommand : IRequest<Response<bool>>
    {
        public string CustomerProfilPicUrl { get; set; }
    }
    public class UpdateCustomerProfileUrlCommandHandler : IRequestHandler<UpdateCustomerProfileUrlCommand, Response<bool>>
    {
        private readonly ICustomerService _customerProfileService;
        private readonly IUserAccessor _userAccessor;

        public UpdateCustomerProfileUrlCommandHandler(ICustomerService customerProfileService, IUserAccessor userAccessor)
        {
            _customerProfileService = customerProfileService;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));

        }

        public async Task<Response<bool>> Handle(UpdateCustomerProfileUrlCommand request, CancellationToken cancellationToken)
        {
            await _customerProfileService.UpdateCustomerProfilePicUrl(request.CustomerProfilPicUrl, _userAccessor.GetUserId());

            // return new Response<int>(product.Id);
            return new Response<bool>(true, $"Customer profile pic url successfully updated: {request.CustomerProfilPicUrl}");

        }
    }
}
