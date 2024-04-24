using covadis.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace covadis.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public static List<User> Users = new List<User>
        {
           new User { Name = "John Doe", Email = "Johndoe@gmail.com", Password = "password123"},
        };

        [HttpGet]
        public IActionResult Get() 
        {
            var userDtos = Users.Select(user => new UserDto
            {
                Name = user.Name,
                Email = user.Email,
            });

            return Ok(userDtos);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            Users.Add(user);
            return Ok();
        }
    }
}
