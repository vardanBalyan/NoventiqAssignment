using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.APP.Services;

namespace NoventiqAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async ValueTask<IActionResult> Register([FromBody] RegisterDto model)
        {
            var result = await _userService.RegisterAsync(model);

            if(result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [HttpPost("login")]
        public async ValueTask<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _userService.LoginUserAsync(model);

            if (result.IsSuccess)
                return Ok(result.Value);

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public async ValueTask<IActionResult> Refresh([FromBody] RefreshTokenDto model)
        {
            var result = await _userService.RefreshTokenAsync(model);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [HttpGet("{username}")]
        public async ValueTask<IActionResult> GetUserByUserName(string username)
        {
            var result = await _userService.GetUserByUserNameAsync(username);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }

        [Authorize]
        [HttpPut("update")]
        public async ValueTask<IActionResult> UpdateUserAsync([FromBody] UpdateUserDto model)
        {
            var result = await _userService.UpdateUserAsync(model);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Error);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("remove/{username}")]
        public async ValueTask<IActionResult> DeleteUserAsync(string username) 
        {
            var result = await _userService.DeleteUserAsync(username);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Error);
        }
    }
}
