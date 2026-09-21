using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CancellationToken token);

        Task<Category?> GetByIdAsync(int id);

        Task<Category> AddAsync(Category category);

        Task<Category?> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByNameAsync(string name);

    }
}
