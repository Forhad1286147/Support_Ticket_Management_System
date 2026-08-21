using Microsoft.EntityFrameworkCore;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Domain.Entities;
using Support_Ticket.Infrastucture.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class TicketCommentRipository:ITicketCommentRepository
    {
        private readonly AppDbContext _context;
        public TicketCommentRipository(AppDbContext context)
        {
            _context = context;    
        }

        public async Task<TicketComment> AddAsync(TicketComment ticketComment)
        {
            try
            {
                await _context.TicketComments.AddAsync(ticketComment);
                await _context.SaveChangesAsync();
                return ticketComment;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingTicketComment = await _context.TicketComments.FindAsync(id);
            if (existingTicketComment != null)
            {
                _context.TicketComments.Remove(existingTicketComment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<TicketComment>> GetAllAsync()
        {
            return await _context.TicketComments.ToListAsync();
        }
        public async Task<TicketComment> GetByIdAsync(int id)
        {
            return await _context.TicketComments.FindAsync(id);
        }
        public async Task<TicketComment> UpdateAsync(TicketComment ticketComment)
        {
            var existingTicketComment = await _context.TicketComments.FindAsync(ticketComment.Id);
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
