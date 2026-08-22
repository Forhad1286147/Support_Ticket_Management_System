using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;

namespace Support_Ticket.Application.Services
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ITicketCommentRepository _repository;

        public TicketCommentService(ITicketCommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TicketComment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TicketComment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TicketComment> AddAsync(
            CreateTicketComment ticketComment)
        {
            var comment = new TicketComment()
            {
                Comment = ticketComment.Comment,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.AddAsync(comment);
        }

        public async Task<TicketComment> UpdateAsync(
            UpdateTicketComment ticketComment)
        {
            var comment = new TicketComment()
            {
                Id = ticketComment.Id,
                Comment = ticketComment.Comment
            };

            return await _repository.UpdateAsync(comment);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

       
    }
}