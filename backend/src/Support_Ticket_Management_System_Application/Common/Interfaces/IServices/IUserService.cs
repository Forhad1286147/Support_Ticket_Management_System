using Microsoft.AspNetCore.Identity;
using Support_Ticket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface IUserService
    {
        Task<List<IdentityUser>> GetAllAscyn();
        Task<IdentityUser> GetByIdAsycn(string userId);
        Task<IdentityUser> AddAsycn(CreateUser user);
        Task<IdentityUser> UpdateAsycn(UpdateUser user);
        Task<bool> DeleteAsycn(string userId);
    }
}
