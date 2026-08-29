using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;

namespace Support_Ticket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var notifications = await _service.GetAllAsync();
            return Ok(notifications);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var notification = await _service.GetByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(CreateNotification notification)
        {
            var newNotification = await _service.AddAsync(notification);
            return CreatedAtAction(nameof(GetById), new { id = newNotification.Id }, newNotification);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateNotification notification)
        {
            var updatedNotification = await _service.UpdateAsync(notification);
            if (updatedNotification == null)
            {
                return NotFound();
            }
            return Ok(updatedNotification);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }   
    }
}
