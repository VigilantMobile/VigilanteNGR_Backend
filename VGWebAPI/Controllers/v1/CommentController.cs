using Application.Features.Comments.Commands.CreateComment;
using Application.Features.Comments.Commands.DeleteComment;
using Application.Features.Comments.Commands.UpdateComment;
using Application.Features.Comments.Queries.GetCommentById;
using Application.Features.Comments.Queries.GetComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace VGWebAPI.Controllers.v1
{
    [Route("api/[controller]")]
    public class CommentController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet("securityTipId")]
        public async Task<IActionResult> Get(int securityTipId, [FromQuery] GetCommentParameter parameter)
        {

            return Ok(await Mediator.Send(new GetCommentsQuery() 
                { SecurityTipId = 2, PageNumber = parameter.PageNumber, PageSize = parameter.PageSize}
            ));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCommentByIdQuery() { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateCommentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateCommentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCommentCommand { Id = id }));
        }
    }
}
