using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> AddAsync(IdentityUser user)
        {
            await _userManager.CreateAsync(user);
            return user;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<List<IdentityUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityUser> UpdateAsync(IdentityUser user)
        {
            await _userManager.UpdateAsync(user);
            return user;
        }
        }
    }
}
