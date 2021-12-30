using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinanceAccounting.Application.Abstractions.Repo;
using FinanceAccounting.Application.Abstractions.Security;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Common.Models;
using FinanceAccounting.Application.Users.Commands.RefreshToken;
using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.Security
{
    public class JwtVerifier : IJwtVerifier
    {
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly IRefreshTokenRepo _refreshTokenRepo;

        public JwtVerifier(TokenValidationParameters tokenValidationParams, IRefreshTokenRepo refreshTokenRepo)
        {
            _tokenValidationParams = tokenValidationParams;
            _refreshTokenRepo = refreshTokenRepo;
        }

        public async Task<(bool result, string errorMessage)> IsTokenValid(RefreshTokenCommand refreshTokenCommand)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                ClaimsPrincipal claimsPrincipal = jwtTokenHandler
                    .ValidateToken(refreshTokenCommand.Token, _tokenValidationParams, out SecurityToken validatedToken);

                if (!IsTokenParamsValid(validatedToken, out string errorMessage))
                {
                    return (false, errorMessage);
                }

                long utcExpiryDate = long.Parse(claimsPrincipal.Claims.First(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                DateTime expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
                if (expiryDate > DateTime.UtcNow)
                {
                    return (false, "Token has not yet expired");
                }

                RefreshToken storedToken = await _refreshTokenRepo.FindByTokenString(refreshTokenCommand.RefreshToken);

                return IsRefreshTokenValid(storedToken, claimsPrincipal, out errorMessage)
                    ? (true, string.Empty)
                    : (false, errorMessage);
            }
            catch (Exception)
            {
                return (false, "Something went wrong");
            }
        }

        private bool IsTokenParamsValid(SecurityToken securityToken, out string errorMessage)
        {
            if (securityToken is JwtSecurityToken jwtSecurityToken)
            {
                if (!_tokenValidationParams.ValidAlgorithms.Any(alg =>
                    alg.Equals(jwtSecurityToken.Header.Alg, StringComparison.InvariantCultureIgnoreCase)))
                {
                    errorMessage = "Encryption algorithm does not match";
                    return false;
                }

                if (!jwtSecurityToken.Issuer.Equals(_tokenValidationParams.ValidIssuer,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    errorMessage = "Token issuer does not match";
                    return false;
                }

                errorMessage = string.Empty;
                return true;
            }

            errorMessage = "Token does not represent JWT";
            return false;
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        private bool IsRefreshTokenValid(RefreshToken storedToken, ClaimsPrincipal claimsPrincipal, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (storedToken == null)
            {
                errorMessage = "Refresh token does not exist";
                return false;
            }

            if (storedToken.ExpiryDate < DateTime.UtcNow)
            {
                errorMessage = "Refresh token has expired";
                return false;
            }

            if (storedToken.IsActive)
            {
                errorMessage = "Refresh token has been used";
                return false;
            }

            if (storedToken.IsRevoked)
            {
                errorMessage = "Refresh token has been revoked";
                return false;
            }

            string jti = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
            if (jti == null || storedToken.JwtId != jti)
            {
                errorMessage = "Token doesn't match";
                return false;
            }

            return true;
        }
    }
}
