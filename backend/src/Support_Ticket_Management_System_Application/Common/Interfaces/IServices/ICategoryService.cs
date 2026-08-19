
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();

        Task<Category?> GetByIdAsync(int id);

        Task<Category> AddAsync(CreateCategory category);

        Task<Category?> UpdateAsync(UpdateCategory category);

        Task<bool> DeleteAsync(int id);
    }
}
