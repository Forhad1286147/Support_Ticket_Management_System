using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Infrastucture.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<IdentityRole>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IdentityRole> GetByIdAsync(string id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<IdentityRole> AddAsync(IdentityRole role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<IdentityRole> UpdateAsync(IdentityRole role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole != null)
            {
                _context.Entry(existingRole).CurrentValues.SetValues(role);
                await _context.SaveChangesAsync();
                return existingRole;
            }
            return null;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            var existingRole = await _context.Roles.FindAsync(id);
            if (existingRole != null)
            {
                _context.Roles.Remove(existingRole);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
