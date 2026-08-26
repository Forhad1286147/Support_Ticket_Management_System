using Microsoft.AspNetCore.Identity;
using Support_Ticket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleByIdAsync(string id);
        Task<IdentityRole> AddRoleAsync(CreateRole role);
        Task<IdentityRole> UpdateRoleAsync(UpdateRole role);
        Task<bool> DeleteRoleAsync(string id);
    }
}
