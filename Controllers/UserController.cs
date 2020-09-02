using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("like")]
        public IActionResult Like(){
            return Ok("Tested Endpoint for Like");
        }
    }
}