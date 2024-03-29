using kvaksy_backend.Data.Models;
using kvaksy_backend.Services;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<LoginResponse>> Login([FromBody] User user)
        {
            if (user == null || user.Email == null || user.Password == null)
            {
                return BadRequest("Email and password are required.");
            }
            try
            {
                var loggedInUser = await _userService.Login(user.Email, user.Password);
                
                return Ok(loggedInUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] User user)
        {
            Globals.CheckForAdminLevelPermission();

            try
            {
                var createdUser = await _userService.CreateAccount(user);

                return Ok(createdUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}