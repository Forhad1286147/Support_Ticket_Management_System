using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<List<IdentityUser>> GetAllAsync();
        Task<IdentityUser> GetByIdAsync(string id);
        Task<IdentityUser> AddAsync(IdentityUser user);
        Task<IdentityUser> UpdateAsync(IdentityUser user);
        Task<bool> DeleteAsync(string id);
    }
}
