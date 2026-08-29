using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<List<IdentityUser>> GetAllAsync();
        Task<IdentityUser> GetByIdAsync(string id);
        Task<IdentityUser> AddAsync(IdentityUser user, string? password = null);
        Task<IdentityUser> UpdateAsync(IdentityUser user, string? password = null);
        Task<bool> DeleteAsync(string id);
    }
}
