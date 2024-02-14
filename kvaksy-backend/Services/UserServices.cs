using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using kvaksy_backend.Data.Models;
using kvaksy_backend.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace kvaksy_backend.Services
{
    public interface IUserServices
    {
        Task<LoginResponse?> Login(string email, string password);
        Task<CreateAccountResponse> CreateAccount(User user);
    }
    public class UserServices : IUserServices
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<LoginResponse?> Login(string email, string password)
        {
            try
            {
                var user = await _userRepository.GetUser(email, password);

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetSection("Security").GetValue<string>("JwtSecret")));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    "https://kvaksy.azurewebsites.net",
                    user.Role.ToString(),
                    new List<Claim>(),
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn);

                var writtenToken = new JwtSecurityTokenHandler().WriteToken(token);

                return new LoginResponse
                {
                    Token = writtenToken,
                    Expires = token.ValidTo,
                    RefreshToken = "Not Implemented Yet"
                };
            }
            catch 
            {
                throw;
            }
        }

        public async Task<CreateAccountResponse> CreateAccount(User user)
        {
            try
            {
                if(!(user.Email == null || user.Password == null || user.Username == null))
                {
                    user.Email = user.Email.ToLower();
                    user.Username = user.Username.ToLower();
                
                    var createdUser = await _userRepository.Create(user);

                    if (createdUser == null)
                    {
                        throw new Exception("Account creation failed");
                    }
                    
                    var login = await Login(user.Email, user.Password);
                        
                    if (login == null)
                    {
                        throw new Exception("Account created, but login failed!");
                    }

                    return new CreateAccountResponse
                    {
                        CreatedUser = user,
                        LoginResponse = login
                    };
                }
                throw new Exception("Email, username and password are required.");
            }
            catch
            {

                throw;
            }
        }

    }
}