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
            var identityRole = new IdentityRole
            {
                Name = role.Name,
                
            };
            return await _roleRepository.AddAsync(identityRole);
        }

        public async Task<IdentityRole> UpdateRoleAsync(UpdateRole role)
        {
            var existingRole = await _roleRepository.GetByIdAsync(role.Id);
            if (existingRole == null)
            {
                throw new Exception("Role not found");
            }
            existingRole.Id = role.Id;
            existingRole.Name = role.Name;
            return await _roleRepository.UpdateAsync(existingRole);
        }

        public async Task<bool> DeleteRoleAsync(string id)
        {
            return await _roleRepository.DeleteAsync(id);
        }
    }
}
