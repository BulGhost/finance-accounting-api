using System.Threading.Tasks;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Domain.Repository
{
    public interface IUserRepo : IRepository<User>
    {
        Task<bool> IsUserWithTheSameEmailAlreadyExists(string email);
        Task<bool> IsUserWithTheSameUserNameAlreadyExists(string userName);
    }
}
