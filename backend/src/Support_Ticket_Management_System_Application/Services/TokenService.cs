using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.Common.Interfaces.IServices;
using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Services
{
    public class TokenService:ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public string GenerateToken(LoginResponseDto loginResponse)
        {
            return _tokenRepository.GenerateToken(loginResponse);
        }
    }
}
