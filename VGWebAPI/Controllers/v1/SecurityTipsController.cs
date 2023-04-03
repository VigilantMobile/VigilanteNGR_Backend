using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Features.AppTroopers.SecurityTips.GetAllSecurityTipCategories;
using Application.Features.AppTroopers.SecurityTips.GetSecurityTipCategoryById;
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
        // User

        [HttpGet("user/{UserId}")] // tips for all user locations:
        public async Task<IActionResult> GetUserSecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter) // for all user locations
        {
            return Ok(await Mediator.Send(new GetUserLocationSecurityTipsByIdQuery {  UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("user/{UserId}/livelocation")] // get tips for user live location
        public async Task<IActionResult> GetSecurityTipsForUserLiveLocation(string UserId, string Coordinates, [FromQuery] GetLiveLocationSecurityTipsQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserLiveLocationSecurityTipsQuery { UserId = UserId, Coordinates = Coordinates, DesiredBroadcastLevel = filter.BroadcastLevel, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        // To Do

        [HttpGet("user/created-by/{UserId}")] //get tips posted by a user.
        public async Task<IActionResult> GetUserPostedSecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserCreatedSecurityTipsByIdQuery { UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        #region * Admin * General
        // Security Tips for State
        [HttpGet("state/{StateId}")] //All tips posted in a state at state level.
        public async Task<IActionResult> GetStateSecurityTips(string StateId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetStateLevelSecurityTipsByIdQuery { StateId = StateId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        // Security Tips for LGA
        [HttpGet("lga/{LGAId}")] //All tips posted in an lga at lga level
        public async Task<IActionResult> GetLGASecurityTips(string LGAId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetLGALevelSecurityTipsByIdQuery { LGAId = LGAId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("town/{TownId}")] //All tips posted in a district 
        public async Task<IActionResult> GetDistrictSecurityTips(string TownId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetDistrictLevelSecurityTipsByIdQuery { DistrictId = TownId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }
        // Security Tip Categories  
        [HttpGet("categories")]
        public async Task<IActionResult> GetSecurityTipCategories([FromQuery] GetAllSecurityTipCategoriesQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllSecurityTipCategoriesQuery { PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("categories/{CategoryId}")] //All tips posted in a district 
        public async Task<IActionResult> GetSecurityTipCategory(string CategoryId)
        {
            return Ok(await Mediator.Send(new GetSecurityTipCategoryByIdQuery { Id = CategoryId }));
        }

        // Security Tip AlertLevelss   
        [HttpGet("alertlevels")]
        public async Task<IActionResult> GetSecurityTipAlertLevels([FromQuery] GetAllAlertLevelsQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllAlertLevelsQuery { PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("categories/{AlertLevelId}")] //All tips posted in a district 
        public async Task<IActionResult> GetSecurityTipAlertLevel(string AlertLevelId)
        {
            return Ok(await Mediator.Send(new GetAlertLevelByIdQuery { AlertLevelId = AlertLevelId }));
        }
        
        #endregion Admin & General
        //

    }
}
