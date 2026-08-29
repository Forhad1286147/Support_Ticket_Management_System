using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Application.Services;

namespace Support_Ticket.Api.Controllers
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
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAscyn();
            return Ok(users);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var user = await _userService.GetByIdAsycn(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync(CreateUser user)
        {
            var newUser = await _userService.AddAsycn(user);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, newUser);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync(UpdateUser user)
        {
            var existingUser = await _userService.GetByIdAsycn(user.Id);
            if (existingUser == null)
            {
                return NotFound();
            }
            var updatedUser = await _userService.UpdateAsycn(user);
            return Ok(updatedUser);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var existingUser = await _userService.GetByIdAsycn(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            var result = await _userService.DeleteAsycn(id);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
