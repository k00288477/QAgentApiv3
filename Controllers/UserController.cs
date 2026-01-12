using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAgentApi.Model;
using QAgentApi.Model.Dto;
using QAgentApi.Service;
using System.Security.Claims;

namespace QAgentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getUserProfile")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfile()
        {
            //Get User Id from Claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User not authorized");
            }

            // Get User by Id
            var user = await _userService.GetUserById(int.Parse(userId));
            // Check if user exists
            if (user == null)
            {
                return NotFound("User Not Found");
            }

            // Map user to DTO (Exclude sensitive information)
            var userProfile = new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                OrganisationId = user.OrganisationId,
                Organisation = user.Organisation
            };
            // return user
            return Ok(userProfile);
        }

    }
}
