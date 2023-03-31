using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using kvaksy_backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace kvaksy_backend.Services
{
    public interface IUserServices
    {
        Task<LoginResponse?> Login(string email, string password);
        Task<ApplicationUser> CreateAccount(ApplicationUser user);
    }
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginResponse?> Login(string email, string password)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);

                var jwtClaims = new List<Claim>
                {
                    userClaims.FirstOrDefault(x =>
                        x.Type == ClaimTypes.Role &&
                        x.Value == "User") ??
                            new Claim(ClaimTypes.Role, "none"),
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetSection("Security").GetValue<string>("JwtSecret")));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    "https://kvaksy.azurewebsites.net",
                    "AppUser",
                    jwtClaims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                var writtenToken = new JwtSecurityTokenHandler().WriteToken(token);

                var response = new LoginResponse
                {
                    Token = writtenToken,
                    Expires = token.ValidTo,
                    RefreshToken = "Not Implemented Yet"
                };

                return response;
            }
            else
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
                    var login = await Login(user.Email, user.Password);
                    if (login == null)
                    {
                        throw new Exception("Account created but failed to login");
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