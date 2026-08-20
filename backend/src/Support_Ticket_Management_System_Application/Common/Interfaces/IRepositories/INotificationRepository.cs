using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllAsync();
        Task<Notification?> GetByIdAsync(int id);
        Task<Notification> AddAsync(Notification notification);
        Task<Notification?> UpdateAsync(Notification notification);
        Task<bool> DeleteAsync(int id);
    }
}
