using AutoMapper;
using FinanceAccounting.Application.Common.Mappings;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto
{
    public class CategoryDto : IMapFrom<Category>
    {
        public int Id { get; set; }
        public OperationType Type { get; set; }
        public string CategoryName { get; set; }

        public void Mapping(Profile profile) => profile.CreateMap<Category, CategoryDto>();
    }
}