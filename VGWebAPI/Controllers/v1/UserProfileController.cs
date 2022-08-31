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

        [HttpGet("get-customer-profile")]
        public async Task<IActionResult> GetCustomerProfileAsync(string CustomerId)
        {
            return Ok(await Mediator.Send(new GetCustomerProfileQuery { CustomerId = CustomerId }));
        }

        #endregion Customers

    }
}
