using Microsoft.EntityFrameworkCore;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Domain.Entities;
using Support_Ticket.Infrastucture.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class TicketRepository:ITicketRepository
    {
        private readonly AppDbContext _context;
        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            try
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
                return ticket;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingTicket = await _context.Tickets.FindAsync(id);
            if (existingTicket != null)
            {
                _context.Tickets.Remove(existingTicket);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.ToListAsync();
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _context.Tickets.FindAsync(id);
        }

        public async Task<Ticket> UpdateAsync(Ticket ticket)
        {
            var existingTicket = await _context.Tickets.FindAsync(ticket.Id);
            if (existingTicket != null)
            {
                _context.Entry(existingTicket).CurrentValues.SetValues(ticket);
                await _context.SaveChangesAsync();
                return ticket;
            }
            throw new InvalidOperationException("Ticket not found");
        }
    }
}
