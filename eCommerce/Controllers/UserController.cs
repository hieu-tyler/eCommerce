using Application.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.System.Users;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var token = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(token)) return BadRequest("Username or password is incorrect");
            return Ok(token);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _userService.Register(request);
            if (!result) return BadRequest("Registration failed");

            return Ok("Registration successful");
        }


        // GET: api/user/paging?PageIndex=1&PageSize=10&keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetUsersPaging([FromQuery] GetUserPagingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var users = await _userService.GetUsersPaging(request);
            return Ok(users);
        }
    }
}
