using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        // private readonly Presenter _presenter;

        // public JWTMiddleware(RequestDelegate next, IConfiguration configuration, Presenter presenter)
        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            // _presenter = presenter;
        }

        // public async Task Invoke(HttpContext context, BL.Facade facade)
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                // attachAccountToContext(context, token, facade);
                attachAccountToContext(context, token);
            
            // Console.WriteLine($"token: {token}");

            await _next(context);
        }

        // private void attachAccountToContext(HttpContext context, string token, BL.Facade facade)
        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                // var jwtToken = (JwtSecurityToken)validatedToken;
                // var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                
                // context.Items["User"] = UserConverter.BLToAPI(_presenter.GetUserById(accountId));
                // Console.WriteLine($"\naccountId = {accountId}\n");
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}