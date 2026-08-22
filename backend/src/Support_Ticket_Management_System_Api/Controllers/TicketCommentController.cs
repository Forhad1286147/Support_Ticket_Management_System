using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System.Diagnostics.Contracts;

namespace Support_Ticket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketCommentController : ControllerBase
    {
        private readonly ITicketCommentService _service;    
        public TicketCommentController(ITicketCommentService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var comments = await _service.GetAllAsync();
            return Ok(comments);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var comment = await _service.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
        [HttpPost("Add")]
        public async Task<ActionResult> Add(CreateTicketComment comment)
        {
            var newComment = await _service.AddAsync(comment);
            return CreatedAtAction(nameof(GetById), new { id = newComment.Id }, newComment);
        }
        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateTicketComment comment)
        {
            var updatedComment = await _service.UpdateAsync(comment);
            if (updatedComment == null)
            {
                return NotFound();
            }
            return Ok(updatedComment);
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
