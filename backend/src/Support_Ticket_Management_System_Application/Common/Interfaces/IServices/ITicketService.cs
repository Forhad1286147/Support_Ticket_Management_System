using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface ITicketService
    {
        public Task<List<Ticket>> GetAllAsync();
        public Task<Ticket> GetAsync(int id);
        public Task<Ticket> AddAsync(CreateTicket ticket);
        public Task<Ticket> UpdateAsync(UpdateTicket ticket);
        public Task<bool> DeleteAsync(int id);
    }
}
