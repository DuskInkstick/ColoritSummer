using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColoritSummer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult Ping()
        {
            return Ok();
        }
    }
}
