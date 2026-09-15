using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Domain.Entities;
using Support_Ticket.Infrastucture.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _user;
        public TicketRepository(AppDbContext context,UserManager<IdentityUser> user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
            if (existingTicket != null)
            {
                existingTicket.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            var query =
    from t in _context.Tickets
    join c in _context.Categories
        on t.CategoryId equals c.Id
    join u in _user.Users
        on t.CreatedBy equals u.Id
    where !t.IsDeleted
    select new Ticket
    {
        Id= t.Id,
        CreatedBy = u.Email,
        Title = t.Title,
        Description = t.Description,
        CreatedAt = t.CreatedAt,
        CategoryId = t.CategoryId,
        Category = c,
        Priority = t.Priority,
        Status = t.Status,
        IsDeleted  = t.IsDeleted



    };

            return await query.ToListAsync();
        }

        public async Task<Ticket?> GetAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<Ticket?> UpdateAsync(Ticket ticket)
        {
            var existingTicket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticket.Id && !t.IsDeleted);
            if (existingTicket != null)
            {
                _context.Entry(existingTicket).CurrentValues.SetValues(ticket);
                await _context.SaveChangesAsync();
                return existingTicket;
            }
            return null;
        }
    }
}
