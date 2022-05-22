using Application.Features.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VGWebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]

    public class UserProfileController : BaseApiController
    {

        #region Customers 

        [HttpGet("get-customer-profile")]
        public async Task<IActionResult> GetCustomerProfileAsync(string CustomerId)
        {
            return Ok(await Mediator.Send(new GetCustomerProfileQuery { CustomerId = CustomerId }));
        }

        #endregion Customers

    }
}
