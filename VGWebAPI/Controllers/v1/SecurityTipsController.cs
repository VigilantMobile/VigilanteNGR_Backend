using Application.DTOs.AlertCategories;
using Application.Features.AppTroopers.SecurityTips;
using Application.Features.AppTroopers.SecurityTips.Commands;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateComment;
using Application.Features.AppTroopers.SecurityTips.Commands.CreateSecurityTip;
using Application.Features.AppTroopers.SecurityTips.Commands.DeleteComment;
using Application.Features.AppTroopers.SecurityTips.Commands.ToggleCommentVote;
using Application.Features.AppTroopers.SecurityTips.Commands.ToggleSecurityTipVote;
using Application.Features.AppTroopers.SecurityTips.Commands.UpdateComment;
using Application.Features.AppTroopers.SecurityTips.GetAllSecurityTipCategories;
using Application.Features.AppTroopers.SecurityTips.GetCategoryTypesWithCategories;
using Application.Features.AppTroopers.SecurityTips.GetSecurityTipCategoryById;
using Application.Features.Location;
using Application.Features.Products.Commands.DeleteProductById;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Wrappers;
using Domain.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VGWebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "SuperAdmin, Admin, Customer")]

    public class SecurityTipsController : BaseApiController
    {
        // User

        [HttpGet("user/{UserId}/registeredcity")] // tips for all user locations:
        public async Task<IActionResult> GetUserRegisteredCitySecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter) // for all user locations
        {
            return Ok(await Mediator.Send(new GetUserLocationSecurityTipsByIdQuery {  UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpGet("user/{UserId}/livelocationcity")] // get tips for user live location
        public async Task<IActionResult> GetSecurityTipsForUserLiveLocation(string UserId, string Coordinates, [FromQuery] GetLiveLocationSecurityTipsQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserLiveLocationSecurityTipsQuery { UserId = UserId, Coordinates = Coordinates, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpPost("user/create")]
        [ProducesResponseType(typeof(Response<CreateSecurityTipResponse>), 200)]
        public async Task<IActionResult> Create([FromBody] CreateSecurityTipCommand command)
        {
            // Get current user's ID if not provided
            if (string.IsNullOrEmpty(command.BroadcasterUserId))
            {
                command.BroadcasterUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }

            // If user didn't provide their current coordinates, we can't validate proximity
            if (string.IsNullOrEmpty(command.UserCurrentCoordinates))
            {
                return BadRequest(new Response<CreateSecurityTipResponse>(
                    null,
                    APIResponseStatus.fail.ToString(),
                    "Current location coordinates are required"));
            }

            var response = await Mediator.Send(command);
            return Ok(response);
        }


        [HttpGet("user/created-by/{UserId}")] //get tips posted by a user.
        public async Task<IActionResult> GetUserPostedSecurityTips(string UserId, [FromQuery] GetSecurityTipsListQueryParameter filter)
        {
            return Ok(await Mediator.Send(new GetUserCreatedSecurityTipsByIdQuery { UserId = UserId, PageNumber = filter.PageNumber, PageSize = filter.PageSize }));
        }

        [HttpPost("tips/{securityTipId}/vote")]
        [ProducesResponseType(typeof(Response<bool>), 200)]
        public async Task<IActionResult> ToggleSecurityTipVote(string securityTipId, [FromBody] ToggleSecurityTipVoteCommand command)
        {
            command.SecurityTipId = securityTipId;
            return Ok(await Mediator.Send(command));
        }

        // Comments 
        /// <summary>
        /// Creates a new comment on a security tip
        /// </summary>
        [HttpPost("{securityTipId}/comments")]
        [ProducesResponseType(typeof(Response<GetSecurityTipResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateComment(string securityTipId, [FromBody] CreateCommentCommand command)
        {
            if (command == null)
                return BadRequest(new Response<GetSecurityTipResponse>(null,
                    APIResponseStatus.fail.ToString(),
                    "Command cannot be null"));

            command.SecurityTipId = securityTipId;
            return Ok(Mediator.Send(command));
        }

        /// <summary>
        /// Updates an existing comment
        /// </summary>
        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> UpdateComment(string commentId, [FromBody] UpdateCommentCommand command)
        {
            if (command == null)
                return BadRequest(new Response<GetSecurityTipResponse>(null,
                    APIResponseStatus.fail.ToString(),
                    "Command cannot be null"));

            command.CommentId = commentId;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a comment from a security tip
        /// </summary>
        [HttpDelete("{securityTipId}/comments/{commentId}")]
        public async Task<IActionResult> DeleteComment(string securityTipId, string commentId)
        {
            var command = new DeleteCommentCommand
            {
                SecurityTipId = securityTipId,
                CommentId = commentId
            };

            var response = await Mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Toggles a vote (upvote/downvote) on a comment
        /// </summary>
        [HttpPost("comments/{commentId}/vote")]
        public async Task<IActionResult> ToggleCommentVote(string commentId, [FromBody] ToggleCommentVoteCommand command)
        {
            if (command == null)
                return BadRequest(new Response<GetSecurityTipResponse>(null,
                    APIResponseStatus.fail.ToString(),
                    "Command cannot be null"));

            command.CommentId = commentId;
            return Ok(await Mediator.Send(command));
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
            return Ok(await Mediator.Send(new GetAllSecurityTipCategoriesQuery {}));
        }

        [HttpGet("categories/{CategoryId}")] //All tips posted in a district 
        public async Task<IActionResult> GetSecurityTipCategory(string CategoryId)
        {
            return Ok(await Mediator.Send(new GetSecurityTipCategoryByIdQuery { Id = CategoryId }));
        }

        [HttpGet("categories-with-types")]
        public async Task<IActionResult> GetCategoryTypesWithCategories()
        {
            var query = new GetCategoryTypesWithCategoriesQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
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
