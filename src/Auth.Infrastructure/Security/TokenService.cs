using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Application.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Security
{
    public class TokenService : ITokenService
    {

        private readonly string _jwtKey;
        private readonly string _jwtIssuer;
        private readonly string _jwtAudience;
        private readonly int _jwtExpirationMinutes;

        public TokenService(
            string jwtKey,
            string jwtIssuer,
            string jwtAudience,
            int jwtExpirationMinutes = 60
        )
        {
            _jwtKey = jwtKey;
            _jwtIssuer = jwtIssuer;
            _jwtAudience = jwtAudience;
            _jwtExpirationMinutes = jwtExpirationMinutes;
        }

        // ---------------------------------------------------------
        // ACCESS TOKEN (JWT)
        // ---------------------------------------------------------
        public string GenerateAccessToken(string email, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };
            
            // Add Roles as Claims
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials:creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ---------------------------------------------------------
        // REFRESH TOKEN
        // ---------------------------------------------------------
        public RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
