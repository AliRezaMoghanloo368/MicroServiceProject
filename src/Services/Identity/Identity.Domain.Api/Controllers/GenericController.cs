using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Domain.Api.Controllers
{
    [ApiController]
    //[Authorize]
    //[HealthCheck]
    public class GenericController : ControllerBase
    {
        public GenericController()
        {

        }
    }
}
