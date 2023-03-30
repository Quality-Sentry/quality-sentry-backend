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
        public async Task<ActionResult<LoginResponse>> Register([FromBody] ApplicationUser user)
        {
            try
            {
                return Ok(await _userService.CreateAccount(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}