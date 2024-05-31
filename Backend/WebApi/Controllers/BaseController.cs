using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator = null;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
