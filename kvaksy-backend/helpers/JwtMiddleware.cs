using kvaksy_backend.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kvaksy_backend.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration, IUserServices userServices)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, configuration, userServices, token);

            await _next.Invoke(context);
        }

        private static void AttachUserToContext(HttpContext context, IConfiguration configuration, IUserServices userServices, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration.GetSection("Security").GetValue<string>("JwtSecret"));

                var decoded = tokenHandler.ReadToken(token);

                var validated = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var claims = validated.Claims.ToList();

                // Get the role from the claims
                claims.ForEach(claim =>
                {
                    if (claim.Type == "aud")
                    {
                        Globals.IsUser = false;
                        Globals.IsAdmin = false;

                        if (claim.Value.ToLower() == "admin")
                        {
                            Globals.IsAdmin = true;
                            return;
                        }

                        if(claim.Value.ToLower() == "user")
                        {
                            Globals.IsUser = true;
                            return;
                        }

                        // Add more roles here

                    }
                });

            }
            catch
            {
                throw;
            }
        }
    }

}