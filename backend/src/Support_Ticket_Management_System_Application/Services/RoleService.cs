using Microsoft.AspNetCore.Identity;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }
        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<IdentityRole> AddRoleAsync(CreateRole role)
        {
            return await _roleRepository.AddAsync(role);
        }

        public async Task<IdentityRole> UpdateRoleAsync(UpdateRole role)
        {
            return await _roleRepository.UpdateAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            return await _roleRepository.DeleteAsync(id);
        }
    }
}
