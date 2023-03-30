using System.Security.Claims;
using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace kvaksy_backend.Services
{
    public interface IUserServices
    {
        Task<LoginResponse> Login(ApplicationUser user, string password);
        Task<ApplicationUser> CreateAccount(ApplicationUser user);
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

        public async Task<ApplicationUser> CreateAccount(ApplicationUser user)
        {
            try
            {
                user.NormalizedEmail = user.Email.ToLower();
                user.NormalizedUserName = user.UserName.ToLower();


                var result = await _userManager.CreateAsync(user, user.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
                    var signin = await _signInManager.CheckPasswordSignInAsync(user, user.Password, false);
                    if (signin.Succeeded)
                    {
                    }
                    return user;
                }
                else
                {
                    var fullMessage = "";
                    for (int i = 0; i < result.Errors.Count(); i++)
                    {
                        fullMessage += (result.Errors.ElementAt(i).Description.ToString() + "\n");
                    }
                    throw new Exception(fullMessage.TrimEnd('\n'));
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

    }
}