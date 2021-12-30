using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Application.Users.Commands.RefreshToken;

namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface IJwtVerifier
    {
        Task<(bool result, string errorMessage)> IsTokenValid(RefreshTokenCommand refreshTokenCommand);
    }
}
