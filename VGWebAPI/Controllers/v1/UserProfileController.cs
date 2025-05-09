﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Location;
using Application.Features.Products.Commands;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProductById;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.UserProfile;
using Application.Features.UserProfile.Commands.UpdateUserProfile;
using Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VGWebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]

    public class UserProfileController : BaseApiController
    {

        #region Customers 

        //[HttpGet("get-customer-profile")]
        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GetCustomerProfileAsync(string CustomerId)
        {
            return Ok(await Mediator.Send(new GetCustomerProfileQuery { CustomerId = CustomerId }));
        }

        [HttpPost("TrustedContacts/add")]
        public async Task<IActionResult> CreateCustomerTrustedContactsAsync([FromBody] CreateCircleMemberViewModel createCustomerTrustedContactsRequest)
        {
            return Ok(await Mediator.Send(new CreateTrustedContactsCommand { createCustomerTrustedContactsRequest = createCustomerTrustedContactsRequest }));
        }

        [HttpPost("TrustedContacts/edit")]
        public async Task<IActionResult> UpdateCustomerTrustedContactAsync([FromBody] UpdateTrustedPersonCommand updateTrustedPersonCommand)
        {
            return Ok(await Mediator.Send(updateTrustedPersonCommand));
        }

        [HttpDelete("TrustedContacts/{id}")]
        public async Task<IActionResult> DeleteCustomerTrustedContactAsync(string id)
        {
            return Ok(await Mediator.Send(new DeleteTrustedPersonByIdCommand { TrustedPersonId = id}));
        }

        [HttpPost("TrustedContacts/acceptinvite")]
        public async Task<IActionResult> AcceptTrustedContactInvitationAsync([FromBody] AcceptTrustedContactInvitationCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("TrustedContacts/rejectinvite")]
        public async Task<IActionResult> RejectTrustedContactInvitationAsync([FromBody] RejectTrustedContactInvitationCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("TrustedContacts/deactivate")]
        public async Task<IActionResult> DeactivateFriendshipAsync([FromBody] DeactivateFriendshipCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("TrustedContacts/reactivate")]
        public async Task<IActionResult> ReactivateFriendshipAsync([FromBody] ReactivateFriendshipCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("toggle-profile-visibility")]
        public async Task<IActionResult> ToggleMemberVisibilityAsync([FromBody] ToggleMemberVisibilityCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("circle/toggle-emergency-contact")]
        public async Task<IActionResult> ToggleEmergencyContact([FromBody] ToggleEmergencyContactCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("circle/emergency-contacts")]
        public async Task<IActionResult> GetEmergencyContacts([FromQuery] string userId)
        {
            var query = new GetEmergencyContactsQuery { userId = userId ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value };
            return Ok(await Mediator.Send(query));
        }

        // New endpoint for updating customer profile
        [HttpPost("update-profile")]
        public async Task<IActionResult> UpdateCustomerProfileAsync([FromBody] UpdateCustomerProfileCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("update-photo-url")]
        public async Task<IActionResult> UpdateCustomerProfilePicUrlAsync([FromBody] UpdateCustomerProfileUrlCommand updateCustomerProfileUrlCommand)
        {
            return Ok(await Mediator.Send(updateCustomerProfileUrlCommand));
        }
        #endregion Customers
    }
}
