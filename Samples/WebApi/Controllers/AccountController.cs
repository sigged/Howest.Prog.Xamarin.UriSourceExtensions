using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleWebApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        protected readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult GetToken()
        {
            //create identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "AllowedUser"),
            };

            var identity = new ClaimsIdentity(claims);

            //generate token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWTConfiguration:SigningKey"]);
            var tokenLifetime = Int32.Parse(_configuration["JWTConfiguration:TokenExpirationMinutes"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Issuer = _configuration["JWTConfiguration:Issuer"],
                Audience = _configuration["JWTConfiguration:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(tokenLifetime),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string encodedToken = tokenHandler.WriteToken(token);

            //respond
            return Ok(new
            {
                Success = true,
                Token = encodedToken
            });
        }

    }
}
