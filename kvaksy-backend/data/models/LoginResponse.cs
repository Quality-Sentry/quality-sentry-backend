using System.IdentityModel.Tokens.Jwt;

namespace kvaksy_backend.Data.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }

    public class CreateAccountResponse
    {
        public ApplicationUser CreatedUser { get; set; }
        public LoginResponse LoginResponse { get; set; }
    }

}
