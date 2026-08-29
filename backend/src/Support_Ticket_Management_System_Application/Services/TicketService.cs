using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support_Ticket.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<Ticket>> GetAllAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _ticketRepository.GetAsync(id);
        }

        public async Task<Ticket> AddAsync(CreateTicket ticket)
        {
            var tick = new Ticket()
            {
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,
                Status = "Open",
                CreatedAt = DateTime.UtcNow.ToString("o")
            };
            return await _ticketRepository.AddAsync(tick);
        }

        public async Task<Ticket> UpdateAsync(UpdateTicket ticket)
        {
            var existingTicket = await _ticketRepository.GetAsync(ticket.Id);
            if (existingTicket == null)
            {
                throw new InvalidOperationException("Ticket not found.");
            }

            if (!string.IsNullOrEmpty(ticket.Title)) existingTicket.Title = ticket.Title;
            if (!string.IsNullOrEmpty(ticket.Description)) existingTicket.Description = ticket.Description;
            if (!string.IsNullOrEmpty(ticket.Priority)) existingTicket.Priority = ticket.Priority;
            if (!string.IsNullOrEmpty(ticket.Status)) existingTicket.Status = ticket.Status;

            return await _ticketRepository.UpdateAsync(existingTicket);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _ticketRepository.DeleteAsync(id);
        }
    }
}
