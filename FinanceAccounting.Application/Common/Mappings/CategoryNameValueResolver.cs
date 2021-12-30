using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Application.Common.DataTransferObjects.OperationDto;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Repository;

namespace FinanceAccounting.Application.Common.Mappings
{
    public class CategoryNameValueResolver : IValueResolver<Operation, OperationDto, string>
    {
        private readonly ICategoryRepo _repo;

        public CategoryNameValueResolver(ICategoryRepo repo)
        {
            _repo = repo;
        }

        public string Resolve(Operation source, OperationDto destination, string destMember, ResolutionContext context)
        {
            return _repo.FindAsync(source.CategoryId).Result.CategoryName;
        }
    }
}
