using Microsoft.AspNetCore.Identity;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return null;
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(
                user,
                request.Password);
            if (!isPasswordValid)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);
            return new LoginResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = roles,
                Token=string.Empty
            };


        }
     }
}
