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
            return new User(1, "Admin", "admin@admin.com");
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers() 
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser()
        {
            var newUser = new User(1, "Gary", "gary@gmail.com");
            await _userService.AddUser(newUser);
            return Ok(newUser);
        }
    }
}
