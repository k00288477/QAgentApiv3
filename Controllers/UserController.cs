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

        [HttpGet]
        public User TestMethod()
        {
            return new User("Admin", "admin@admin.com");
        }

    }
}
