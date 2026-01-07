using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QAgentApi.Model;
using QAgentApi.Model.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QAgentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        // for testing purposes
        public static User user = new User();

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Email = request.Email;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            // Compare emails
            if(user.Email != request.Email)
            {
                return BadRequest("Wrong Email or Password.");
            }
            // compare hashed password with the request password
            if(new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong Email or Password.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email) // Unique key for users
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config.GetValue<string>("AppSetting:Token")!) // need to add this
                );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); // 512 bit string token

            var tokenDescripter = new JwtSecurityToken(
                issuer: config.GetValue<string>("AppSettings:Issuer"), // TODO
                audience: config.GetValue<string>("AppSettings:Audience"), // TODO
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1), // valid for 24 hrs after request
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescripter);
        }



    }
}
