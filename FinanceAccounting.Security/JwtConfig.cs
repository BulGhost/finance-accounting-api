using FinanceAccounting.Application.Abstractions.Security;

namespace FinanceAccounting.Security
{
    public class JwtConfig : IJwtConfig
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningAlgorithm { get; set; }
    }
}
