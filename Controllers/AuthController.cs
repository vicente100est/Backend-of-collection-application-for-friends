using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.Models.Entity;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Admin")]
        public IActionResult AuthenticationAdmin([FromBody] AdminAuthEntity adminModel)
        {
            return Ok(adminModel);
        }

        [HttpPost("User")]
        public IActionResult AuthenticationUser([FromBody] UserAuthEntity userModel)
        {
            return Ok(userModel);
        }
    }
}
