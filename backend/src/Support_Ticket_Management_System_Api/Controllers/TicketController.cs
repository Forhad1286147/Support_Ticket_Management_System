using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;

namespace Support_Ticket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;
        public TicketController(ITicketService service  )
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Ticket>>> GetAllTickets()
        {
            var tickets = await _service.GetAllAsync();
            return Ok(tickets);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketById(int id)
        {
            var ticket = await _service.GetAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Ticket>> AddTicket(CreateTicket ticket)
        {
            var newTicket = await _service.AddAsync(ticket);
            return CreatedAtAction(nameof(GetTicketById), new { id = newTicket.Id }, newTicket);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Ticket>> UpdateTicket(int id, UpdateTicket ticket)
        {
            ticket.Id = id;
            var existingTicket = await _service.GetAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            var updatedTicket = await _service.UpdateAsync(ticket);
            return Ok(updatedTicket);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var existingTicket = await _service.GetAsync(id);
            if (existingTicket == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
