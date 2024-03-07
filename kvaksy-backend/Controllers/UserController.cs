using kvaksy_backend.data.models;
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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LoginResponse>> LoginUser([FromBody] LoginCredentials credentials)
        {
            if (credentials == null || credentials.Email == null || credentials.Password == null)
            {
                return BadRequest("Email and password are required.");
            }
            try
            {
                var loggedInUser = await _userService.Login(credentials.Email, credentials.Password);
                
                return Ok(loggedInUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<LoginResponse>> CreateUser([FromBody] User user)
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