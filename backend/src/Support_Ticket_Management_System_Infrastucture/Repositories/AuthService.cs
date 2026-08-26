using Microsoft.AspNetCore.Identity.Data;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _authRepository;
        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            return await _authRepository.LoginAsync(request);
        }
    }
}
