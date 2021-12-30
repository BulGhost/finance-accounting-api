namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface IJwtConfig
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        string SigningAlgorithm { get; set; }
    }
}
