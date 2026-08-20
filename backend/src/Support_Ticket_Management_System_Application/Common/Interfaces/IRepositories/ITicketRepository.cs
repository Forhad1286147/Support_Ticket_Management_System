using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();
        Task<Ticket> GetAsync(int id);
        Task<Ticket> AddAsync(Ticket ticket);
        Task<Ticket> UpdateAsync(Ticket ticket);
        Task<bool> DeleteAsync(int id);
    }
}
