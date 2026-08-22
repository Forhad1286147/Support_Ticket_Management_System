using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface ITicketCommentService
    {
        Task<List<TicketComment>> GetAllAsync();
        Task<TicketComment> GetByIdAsync(int id);
        Task<TicketComment> AddAsync(CreateTicketComment ticketComment);
        Task<TicketComment> UpdateAsync(UpdateTicketComment ticketComment);
        Task<bool> DeleteAsync(int id);
        
    }
}
