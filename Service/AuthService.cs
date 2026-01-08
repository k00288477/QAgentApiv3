

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QAgentApi.Model;
using QAgentApi.Model.Dto;
using QAgentApi.Repository.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QAgentApi.Service
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        public AuthService(UserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            if(await _userService.GetUserByEmail(request.Email) != null)
            {
                return null; // Return null, react in the controller
            }
            var user = new User();
            var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);

            user.Email = request.Email;
            user.PasswordHash = hashedPassword;

            await _userService.AddUser(user);

            return user;
        }

        public async Task<string> LoginAysnc(UserDto request)
        {
            var user = await _userService.GetUserByEmail(request.Email);
            if(user == null)
            {
                return null; // handle in controller
            }

            // compare hashed password with the request password
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null; // handle in controller
            }

            return CreateToken(user);
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) 
                // Unique key for users
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSetting:Token")!)
                );

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512); // 512 bit string token

            var tokenDescripter = new JwtSecurityToken(
                issuer: _config.GetValue<string>("AppSettings:Issuer"),
                audience: _config.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1), // valid for 24 hrs after request
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescripter);
        }

    }
}
