using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Services
{
    public class AuthService:IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(
            LoginRequestDto request)
        {
            var result = await _authRepository.LoginAsync(request);

            if (result == null)
            {
                return null;
            }

            result.Token = _tokenService.GenerateToken(result);

            return result;
        }
    }
}
