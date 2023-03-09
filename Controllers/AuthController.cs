using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagos.Backend.DTO;
using Pagos.Backend.Models.Entity;
using Pagos.Backend.Services.Auth.IService;

namespace Pagos.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public GenericDTO response;
        private readonly IAdminService _adminAuth;
        private readonly IUserService _userAuth;

        public AuthController(GenericDTO response, IAdminService adminService, IUserService userService)
        {
            this._adminAuth = adminService;
            this._userAuth = userService;
            this.response = response;
        }

        [HttpPost("Admin")]
        public IActionResult AuthenticationAdmin([FromBody] AdminAuthEntity adminModel)
        {
            var adminResponse = _adminAuth.Auth(adminModel);

            if (adminResponse == null)
            {
                response.Message = "Ups, los datos ingresados son incorrectos";
                return BadRequest(response);
            }

            response.Success = 1;
            response.Data = adminResponse;

            return Ok(response);
        }

        [HttpPost("User")]
        public IActionResult AuthenticationUser([FromBody] UserAuthEntity userModel)
        {
            var userResponse = _userAuth.Auth(userModel);

            if (userResponse == null)
            {
                response.Message = "Ups, los datos ingresados son incorrectos";
            }

            response.Success = 1;
            response.Data = userResponse;

            return Ok(response);
        }
    }
}
