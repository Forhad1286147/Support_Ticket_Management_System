using Microsoft.AspNetCore.Identity;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Support_Ticket.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<IdentityUser>> GetAllAscyn()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IdentityUser> GetByIdAsycn(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IdentityUser> AddAsycn(CreateUser user)
        {
            var newUser = new IdentityUser
            {
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = true
            };
            return await _userRepository.AddAsync(newUser, user.Password);
        }

        public async Task<IdentityUser> UpdateAsycn(UpdateUser user)
        {
            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.Phone;

            return await _userRepository.UpdateAsync(existingUser, user.Password);
        }

        public async Task<bool> DeleteAsycn(string id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }
            return await _userRepository.DeleteAsync(id);
        }
    }
}
