using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QAgentApi.Model;
using QAgentApi.Service;

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

        
        [HttpGet("Test")]
        public string TestMethod()
        {
            return "Api reached succesfully.";
        }

    }
}
