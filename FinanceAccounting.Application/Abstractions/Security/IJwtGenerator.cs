using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface IJwtGenerator
    {
        Task<UserAuthenticationResponse> CreateTokensAsync(User user);
    }
}
