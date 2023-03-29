using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace kvaksy_backend.Services
{
    public interface IUserServices
    {
        Task<LoginResponse> Login(ApplicationUser user, string password);
        Task<ApplicationUser> CreateAccount(ApplicationUser user, string password);
    }
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginResponse> Login(ApplicationUser user, string password)
        {
            // var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            // if(result.Succeeded)
            // {
            //     var token = await GenerateJwtToken(user);
            //     var refreshToken = await GenerateRefreshToken(user);
            //     return new LoginResponse
            //     {
            //         Token = token,
            //         RefreshToken = refreshToken,
            //         Expires = DateTime.Now.AddMinutes(30)
            //     };
            // }
            // else
            {
                return null;
            }
        }

        public async Task<ApplicationUser> CreateAccount(ApplicationUser user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                }

                return user;

            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}