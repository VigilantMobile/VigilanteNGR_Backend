using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands;
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
    [Authorize(Roles = "SuperAdmin, Admin, Customer")]

    public class SecurityTipsController : BaseApiController
    {
        [HttpGet("user/{UserId}")] 
        public async Task<IActionResult> GetUserSecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter) // for all user locations
        {
            return Ok(await Mediator.Send(new GetUserCreatedSecurityTipsByIdQuery {  UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        // To Do
        [HttpGet("user/created-by/{UserId}")] //get tips posted by a user.
        public async Task<IActionResult> GetUserPostedSecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserCreatedSecurityTipsByIdQuery { UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("user/created-by/{UserId}/{Coordinates}")] //get tips posted by a user.
        public async Task<IActionResult> GetUserPostedSecurityTips(string UserId, [FromQuery] GetLiveLocationSecurityTipsQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserLiveLocationSecurityTipsQuery { UserId = UserId, coordinates = filter.Coordinates, DesiredBroadcastLevel = filter.BroadcastLevel, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("state/{StateId}")] //All tips posted in a state at state level.
        public async Task<IActionResult> GetStateSecurityTips(string StateId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetStateLevelSecurityTipsByIdQuery { StateId = StateId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("lga/{LGAId}")] //All tips posted in an lga at lga level
        public async Task<IActionResult> GetLGASecurityTips(string LGAId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetLGALevelSecurityTipsByIdQuery { LGAId = LGAId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("town/{TownId}")] //Al tips posted in a district 
        public async Task<IActionResult> GetDistrictSecurityTips(string TownId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetDistrictLevelSecurityTipsByIdQuery { DistrictId = TownId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }
    }
}
