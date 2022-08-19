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
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class SecurityTipsController : BaseApiController 
    {
        [HttpGet("user-security-tips/{UserId}")]
        public async Task<IActionResult> GetUserSecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserCreatedSecurityTipsByIdQuery {  UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("state-security-tips/{StateId}")]
        public async Task<IActionResult> GetStateSecurityTips(int StateId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetStateLevelSecurityTipsByIdQuery { StateId = StateId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("lga-security-tips/{LGAId}")]
        public async Task<IActionResult> GetLGASecurityTips(int LGAId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetLGALevelSecurityTipsByIdQuery { LGAId = LGAId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("districts-security-tips/{DistrictId}")]
        public async Task<IActionResult> GetDistrictSecurityTips(int DistrictId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetDistrictLevelSecurityTipsByIdQuery { DistrictId = DistrictId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        } 

    }
}
