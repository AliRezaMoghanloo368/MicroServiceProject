using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Api.Controllers
{
    [ApiController]
    public class GenericController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public GenericController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
