using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FinanceAccounting.Application.Abstractions.Repo;
using FinanceAccounting.Application.Abstractions.Security;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Common.Models;
using FinanceAccounting.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.Security
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly string _signingAlgorithm;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SecurityKey _securityKey;
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public JwtGenerator(IJwtSigningEncodingKey signingEncodingKey, IRefreshTokenRepo refreshTokenRepo)
        {
            _signingAlgorithm = signingEncodingKey.JwtConfig.SigningAlgorithm;
            _issuer = signingEncodingKey.JwtConfig.Issuer;
            _audience = signingEncodingKey.JwtConfig.Audience;
            _securityKey = signingEncodingKey.GetKey();
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<UserAuthenticationResponse> CreateTokensAsync(User user)
        {
            SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            RefreshToken refreshToken = CreateRefreshToken(token.Id, user.Id);
            await _refreshTokenRepo.AddAsync(refreshToken);

            return new UserAuthenticationResponse
            {
                UserName = user.UserName,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(User user)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Name, user.UserName),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var credentials = new SigningCredentials(_securityKey, _signingAlgorithm);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = credentials,
                Issuer = _issuer,
                Audience = _audience
            };
        }

        private RefreshToken CreateRefreshToken(string tokenId, int userId)
        {
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            randomNumberGenerator.GetBytes(randomBytes);

            return new()
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomBytes),
                JwtId = tokenId,
                IsActive = false,
                IsRevoked = false,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(3)
            };
        }
    }
}
