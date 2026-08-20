using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAllAsync();
        Task<Notification?> GetByIdAsync(int id);
        Task<Notification> AddAsync(CreateNotification notification);
        Task<Notification> UpdateAsync(UpdateNotification notification);
        Task<bool> DeleteAsync(int id);
    }
}
