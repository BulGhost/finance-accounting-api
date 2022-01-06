using System.Threading;
using System.Threading.Tasks;
using FinanceAccounting.Application.Common.DataTransferObjects.UserDto;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Abstractions.Security
{
    public interface ITokenGenerator
    {
        Task<UserAuthenticationResponse> CreateTokensAsync(User user, CancellationToken cancellationToken);
    }
}
