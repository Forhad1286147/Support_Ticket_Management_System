using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
