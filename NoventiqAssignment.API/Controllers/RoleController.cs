using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.APP.Services;

namespace NoventiqAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize]
        [HttpGet]
        public async ValueTask<IActionResult> GetAllRolesAsync()
        { 
            var roles = await _roleService.GetAllRolesAsync();

            return Ok(roles.Value);
        }

        [Authorize]
        [HttpGet("{name}")]
        public async ValueTask<IActionResult> GetRolesByNameAsync(string name)
        {
            var result = await _roleService.GetRolesByNameAsync(name);

            if(result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async ValueTask<IActionResult> CreateRoleAsync([FromBody] RoleDto model)
        {
            var result = await _roleService.CreateRoleAsync(model);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Error);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async ValueTask<IActionResult> UpdateRoleAsync([FromBody] UpdateRoleDto model)
        {
            var result = await _roleService.UpdateRoleAsync(model);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Error);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async ValueTask<IActionResult> DeleteRoleAsync([FromBody] RoleDto model)
        {
            var result = await _roleService.DeleteRoleAsync(model);

            if (result.IsSuccess)
                return NoContent();

            return BadRequest(result.Error);
        }
    }
}
