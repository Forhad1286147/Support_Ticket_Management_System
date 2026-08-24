using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Application.Services;

namespace Support_Ticket.Api.Controllers
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
    }
}
