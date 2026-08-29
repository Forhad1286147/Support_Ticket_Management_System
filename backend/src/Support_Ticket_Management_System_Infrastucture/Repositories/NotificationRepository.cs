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
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;
        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            return notification;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);
            if (existingNotification != null)
            {
                existingNotification.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _context.Notifications.Where(n => !n.IsDeleted).ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int id)
        {
            return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);
        }

        public async Task<Notification?> UpdateAsync(Notification notification)
        {
            var existingNotification = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == notification.Id && !n.IsDeleted);
            if (existingNotification != null)
            {
                _context.Entry(existingNotification).CurrentValues.SetValues(notification);
                await _context.SaveChangesAsync();
                return existingNotification;
            }
            return null;
        }
    }
}
