using Application.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace VGWebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response<>), StatusCodes.Status500InternalServerError)]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
