using covadis.Api.Services;
using covadis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController(AuthService authService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = authService.Login(request);

            if (response == null)
            {
                return Unauthorized();
            }

            return Ok(response);
        }
    }
}
