using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BL;
using API.Helpers;
using API.DTO;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly IConfiguration _configuration;

        private BL.Facade _facade;

        public AuthController(IConfiguration configuration, BL.Facade facade)
        {
            _configuration = configuration;
            _facade = facade;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost]
        public IActionResult Auth([FromBody] UserToLogin user)
        {
            try
            {
                var userInfo = _facade.LogIn(user.Login, user.Password);
			    // Console.WriteLine("String 2");
                var tokenString = GenerateJwtToken(userInfo.Id, user.Login, userInfo.UserType);

                // Console.WriteLine("String 3");

                return Ok(new { Token = tokenString, Message = "Success" });
            }
            catch (Exception exc)
            {
                Console.WriteLine($"AUTH EXCEPTION: {exc}\n");
                if (exc.GetType().IsAssignableFrom(typeof(AccessPermissionsException)))
                {
                    return Unauthorized("You are blocked"); 
                }
                else
                {
                    return BadRequest("Please pass the valid Login and Password" + exc); 
                }
            }
        }

        // [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        // [Produces("application/json")]
        // [HttpGet(nameof(GetResult))]
        // public IActionResult GetResult()
        // {
        //     return Ok("API Validated");
        // }

        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="login"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GenerateJwtToken(int id, string login, Permissions role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            // Console.WriteLine($"G 2\n");

            // Console.WriteLine($"key = {key}");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Subject = new ClaimsIdentity(new[] { new Claim("id", login) }),
                Subject = new ClaimsIdentity(new Claim[] 
                    {
                        // new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                        new Claim(ClaimTypes.Name, login),
                        new Claim(ClaimTypes.Role, role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}