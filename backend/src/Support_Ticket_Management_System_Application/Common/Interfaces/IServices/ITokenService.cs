using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Application.Common.Interfaces.IServices
{
    public interface ITokenService
    {
        string GenerateToken(LoginResponseDto loginResponse);
    }
}
