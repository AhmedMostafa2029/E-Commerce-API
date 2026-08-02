using E_Commerce.Application.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class TokenServices(IOptions<JwtSettings> options) : ITokenServices
    {
        private readonly JwtSettings settings = options.Value;

        public string CreateToken(string userId, string email, string userName, IEnumerable<string> roles)
        {
            //private Claims => User
            var Claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Name , userName),
            };
            Claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Header

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: Claims,
                expires: DateTime.Now.AddMinutes(settings.ExpirationByMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // الخلااااط

        }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int ExpirationByMinutes { get; set; } = 60;

    }

}
