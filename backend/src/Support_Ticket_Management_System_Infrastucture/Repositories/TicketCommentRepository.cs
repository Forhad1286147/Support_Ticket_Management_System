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
    public class TicketCommentRepository : ITicketCommentRepository
    {
        private readonly AppDbContext _context;
        public TicketCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TicketComment> AddAsync(TicketComment ticketComment)
        {
            await _context.TicketComments.AddAsync(ticketComment);
            await _context.SaveChangesAsync();
            return ticketComment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingTicketComment = await _context.TicketComments.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (existingTicketComment != null)
            {
                existingTicketComment.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TicketComment>> GetAllAsync()
        {
            return await _context.TicketComments.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<TicketComment?> GetByIdAsync(int id)
        {
            return await _context.TicketComments.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<TicketComment?> UpdateAsync(TicketComment ticketComment)
        {
            var existingTicketComment = await _context.TicketComments.FirstOrDefaultAsync(c => c.Id == ticketComment.Id && !c.IsDeleted);
            if (existingTicketComment != null)
            {
                _context.Entry(existingTicketComment).CurrentValues.SetValues(ticketComment);
                await _context.SaveChangesAsync();
                return existingTicketComment;
            }
            return null;
        }
    }
}
