using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;

namespace Support_Ticket.Api.Controllers
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddRoleAsync([FromBody] CreateRole role)
        {
            var addedRole = await _roleService.AddRoleAsync(role);
            return CreatedAtAction(nameof(GetRoleByIdAsync), new { id = addedRole.Id }, addedRole);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateRoleAsync(string id, [FromBody] UpdateRole role)
        {
            var updatedRole = await _roleService.UpdateRoleAsync(role);
            if (updatedRole == null)
            {
                return NotFound();
            }
            return Ok(updatedRole);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            var deleted = await _roleService.DeleteRoleAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
