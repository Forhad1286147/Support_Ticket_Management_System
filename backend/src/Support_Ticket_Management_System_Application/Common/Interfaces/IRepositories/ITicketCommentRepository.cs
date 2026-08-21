using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface ITicketCommentRepository
    {
        Task<List<TicketComment>>GetAllAsync();
        Task<TicketComment>GetByIdAsync(int id);
        Task<TicketComment>AddAsync(TicketComment ticketComment);
        Task<TicketComment>UpdateAsync(TicketComment ticketComment);
        Task<bool>DeleteAsync(int id);

    }
}
