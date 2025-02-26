using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Location;
using Application.Features.Products.Commands;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProductById;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.UserProfile;
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
        public async Task<IActionResult> CreateCustomerTrustedContactsAsync([FromBody] CreateCustomerTrustedContactViewModel createCustomerTrustedContactsRequest)
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

        [HttpPost("update-photo-url")]
        public async Task<IActionResult> UpdateCustomerProfilePicUrlAsync([FromBody] UpdateCustomerProfileUrlCommand updateCustomerProfileUrlCommand)
        {
            return Ok(await Mediator.Send(updateCustomerProfileUrlCommand));
        }
        #endregion Customers
    }
}
