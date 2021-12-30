using Microsoft.IdentityModel.Tokens;

namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
