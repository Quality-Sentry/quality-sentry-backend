using kvaksy_backend.Data.Models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace kvaksy_backend.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;
        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        [Route("login")]
        [HttpPost]
        public ActionResult<LoginResponse> Login([FromBody] ApplicationUser user)
        {
            try
            {
                return Ok(new LoginResponse());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("register")]
        [HttpPost]
        public ActionResult<LoginResponse> Register([FromBody] ApplicationUser user, string password)
        {
            try
            {
                return Ok(_userService.CreateAccount(user, password));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}