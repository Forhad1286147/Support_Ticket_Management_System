
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;

namespace Support_Ticket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var categories = await _service.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost("Add")]
        public async Task<ActionResult> Add(CreateCategory category)
        {
            var newCategory = await _service.AddAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateCategory category)
        {
            var updatedCategory = await _service.UpdateAsync(category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
