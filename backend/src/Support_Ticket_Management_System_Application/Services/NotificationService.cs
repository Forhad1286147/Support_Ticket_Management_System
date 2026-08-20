using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetAllAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }
        public async Task<Notification> AddAsync(CreateNotification notification)
        {
            var notifi = new Notification()
            {
               
                Message = notification.Message
               
            };
            return await _notificationRepository.AddAsync(notifi);
        }
        public async Task<Notification> UpdateAsync(UpdateNotification notification)
        {
            var notifi = new Notification()
            {
                Id = notification.Id,
                Message = notification.Message,
                IsRead = notification.IsRead
              
            };
            return await _notificationRepository.UpdateAsync(notifi);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _notificationRepository.DeleteAsync(id);
        }
    }
}
