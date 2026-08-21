using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Services
{
    public class TicketCommentServicecs: ITicketCommentService
    {
        private readonly ITicketCommentRepository _service;
        public TicketCommentServicecs(ITicketCommentRepository service)
        {
            _service = service;
        }

        public async Task<List<TicketComment>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }
        public async Task<TicketComment> GetByIdAsync(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        public async Task<TicketComment> AddAsync(CreateTicketComment ticketComment)
        {
            var comment = new TicketComment
            {
               
                Comment = ticketComment.Comment,
                CreatedAt = ticketComment.CreatedAt
            };
            return await _service.AddAsync(comment);
        }

        public async Task<TicketComment> UpdateAsync(UpdateTicketComment ticketComment)
        {
            var comment = new TicketComment
            {
                Id = ticketComment.Id,
                Comment = ticketComment.Comment,
                CreatedAt = ticketComment.CreatedAt
            };

            return await _service.UpdateAsync(comment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _service.DeleteAsync(id);
        }
       

    }
}
