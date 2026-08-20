using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Services   
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }
        public async Task<Category> AddAsync(CreateCategory category)
        {
            var categorys = new Category
            {
                Name = category.Name,
                IsActive = true
            };
            return await _repo.AddAsync(categorys);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
           return await _repo.GetByIdAsync(id);
        }

        public async Task<Category?> UpdateAsync(UpdateCategory category)
        {
           var cat = new Category
           {
               Id = category.Id,
               Name = category.Name,
               IsActive = category.IsActive
           };
            return await _repo.UpdateAsync(cat);
        }
    }
}
