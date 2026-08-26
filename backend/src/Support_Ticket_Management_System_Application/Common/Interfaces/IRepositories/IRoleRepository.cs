using Microsoft.AspNetCore.Identity;
using Support_Ticket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        Task<List<IdentityRole>> GetAllAsync();
        Task<IdentityRole> GetByIdAsync(string id);
        Task<IdentityRole> AddAsync(IdentityRole role);
        Task<IdentityRole> UpdateAsync(IdentityRole role);
        Task<bool> DeleteAsync(string id);
      
    }
}
