using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Common.Models;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Infrastructure.Persistence.Security
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly byte[] _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _accessTokenExpirationInMinutes;
        private readonly int _refreshTokenExpirationInDays;

       public JwtTokenService(IConfiguration configuration)
{
    ArgumentNullException.ThrowIfNull(configuration);

    var jwtKey = configuration["Jwt:Key"];
    if (string.IsNullOrWhiteSpace(jwtKey))
    {
        throw new InvalidOperationException("JWT key is not configured.");
    }

    _key = Encoding.UTF8.GetBytes(jwtKey);

    _issuer = configuration["Jwt:Issuer"]
        ?? throw new InvalidOperationException("JWT Issuer is not configured.");

    _audience = configuration["Jwt:Audience"]
        ?? throw new InvalidOperationException("JWT Audience is not configured.");

    _accessTokenExpirationInMinutes =
        int.TryParse(configuration["Jwt:AccessTokenExpirationInMinutes"], out var accessExp)
            ? accessExp
            : 30;

    _refreshTokenExpirationInDays =
        int.TryParse(configuration["Jwt:RefreshTokenExpirationInDays"], out var refreshExp)
            ? refreshExp
            : 7;
}


        public string GenerateAccessToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),                
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString()),

            };

            var key = new SymmetricSecurityKey(_key);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_accessTokenExpirationInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                }, out _);

                return true;
            }
            catch ( SecurityTokenInvalidSignatureException )
            {
                return false;
            }
            catch (SecurityTokenException )
            {
                return false;
            }
        }

        [SuppressMessage(
    "Security",
    "CA5404:Do not disable token validation checks",
    Justification = "Required to extract claims from expired access token during refresh-token flow")]
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_key),
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    ValidateLifetime = false, 
                    ClockSkew = TimeSpan.Zero
                }, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtToken ||
                    !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                return principal;
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }




        public TokenResponse GenerateTokens(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new TokenResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = GenerateRefreshToken(),
                AccessTokenExpiryTime = DateTime.UtcNow.AddMinutes(_accessTokenExpirationInMinutes),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_refreshTokenExpirationInDays)
            };
        }

    }
}
