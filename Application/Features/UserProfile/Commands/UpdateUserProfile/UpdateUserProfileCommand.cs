using Application.Interfaces;
using Application.Services.Interfaces.UserProfile;
using Application.Wrappers;
using Domain.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfile.Commands.UpdateUserProfile
{
    public partial class UpdateCustomerProfileCommand : IRequest<Response<bool>>
    {
        public string CustomerId { get; set; }
        public string FullName { get; set; }
        public string Coordinates { get; set; }
    }

    public class UpdateCustomerProfileCommandHandler : IRequestHandler<UpdateCustomerProfileCommand, Response<bool>>
    {
        private readonly ICustomerService _customerService;
        private readonly IUserAccessor _userAccessor;

        public UpdateCustomerProfileCommandHandler(ICustomerService customerService, IUserAccessor userAccessor)
        {
            _customerService = customerService;
            _userAccessor = userAccessor ?? throw new ArgumentNullException(nameof(userAccessor));
        }

        public async Task<Response<bool>> Handle(UpdateCustomerProfileCommand request, CancellationToken cancellationToken)
        {
            var updateViewModel = new UpdateCustomerProfileViewModel
            {
                CustomerId = request.CustomerId,
                FullName = request.FullName,
                Coordinates = request.Coordinates
            };

            bool result = await _customerService.UpdateCustomerProfileAsync(updateViewModel);
            return new Response<bool>(result, responsestatus: APIResponseStatus.success.ToString(), "Customer profile successfully updated.");
        }
    }
}
