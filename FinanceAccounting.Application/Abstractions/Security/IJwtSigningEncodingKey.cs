using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface IJwtSigningEncodingKey
    {
        IJwtConfig JwtConfig { get; }
        SecurityKey GetKey();
    }
}
