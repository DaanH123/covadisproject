using covadis.Api.Models;
using covadis.Api.Services;
using covadis.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace covadis.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController(UserService userService) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var response = userService.GetUserById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult CreateUser(CreateUserRequest request)
        {
            var response = userService.CreateUser(request);

            return CreatedAtAction(nameof(CreateUser), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var updatedUser = userService.UpdateUser(id, user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpGet("secret")]
        public IActionResult Secret()
        {
            return Ok("This is a secret message");
        }
    }
}
