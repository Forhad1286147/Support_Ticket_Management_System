using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Support_Ticket.Infrastucture.Repositories
{
    public class TokenRepository:ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(LoginResponseDto loginResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    loginResponse.UserId),

                new Claim(
                    ClaimTypes.Email,
                    loginResponse.Email)
            };

            foreach (var role in loginResponse.Roles)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
