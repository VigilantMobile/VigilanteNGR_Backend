using Application.Features.Location;
using Application.Features.Products.Commands.DeleteProductById;
using Application.Features.Products.Commands.UpdateProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VGWebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class LocationController : BaseApiController
    {

        #region Districts 
        [HttpGet("get-all-districts")]
        public async Task<IActionResult> GetAllDistrictsAsync([FromQuery] GetAllDistrictsParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllDistrictsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        [HttpGet("district/{id}")]
        public async Task<IActionResult> GetDistrictAsync(int id)
        {
            return Ok(await Mediator.Send(new GetDistrictByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost("create-district")]
        public async Task<IActionResult> CreateDistrict(CreateDistrictCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")] 
        [Authorize]
        public async Task<IActionResult> UpdateDistrict(int id, UpdateDistrictCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        #endregion district


        #region LGA
        [HttpGet("lga/{id}")]
        public async Task<IActionResult> GetLGAAsync(int id)
        {
            return Ok(await Mediator.Send(new GetLGAByIdQuery { Id = id }));
        }

        [HttpGet("lga/{id}/districts")]
        public async Task<IActionResult> GetDistrictsinLGAAsync(int id, [FromQuery] GetDistrictsinLGAParameter filter)
        {
            return Ok(await Mediator.Send(new GetDistrictsinLGAQuery { LGAId = id }));
        }
        #endregion lga

        #region State
        [HttpGet("state/{id}")]
        public async Task<IActionResult> GetStateAsync(int id)
        {
            return Ok(await Mediator.Send(new GetStateByIdQuery { Id = id }));
        }

        [HttpGet("state/{id}/lgas")]
        public async Task<IActionResult> GetLGAsInStateAsync(int id, [FromQuery] GetLGAsinStateParameter filter)
        {
            return Ok(await Mediator.Send(new GetLGAsinStateQuery { StateId = id }));
        }
        #endregion State

    }
}
