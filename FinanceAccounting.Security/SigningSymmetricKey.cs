using System.Text;
using FinanceAccounting.Application.Abstractions.Security;
using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.Security
{
    public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;

        public IJwtConfig JwtConfig { get; }

        public SigningSymmetricKey(IJwtConfig jwtConfig)
        {
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
            JwtConfig = jwtConfig;
        }

        public SecurityKey GetKey() => _secretKey;
    }
}
