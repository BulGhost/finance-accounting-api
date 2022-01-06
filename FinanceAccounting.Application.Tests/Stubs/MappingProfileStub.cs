using System.Linq;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects.CategoryDto;
using FinanceAccounting.Application.Common.DataTransferObjects.OperationDto;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Tests.Stubs
{
    public class MappingProfileStub : Profile
    {
        public MappingProfileStub()
        {
            var categoriesRepo = new CategoryRepoStub().GetAllAsync().Result;
            CreateMap<Category, CategoryDto>();
            CreateMap<Operation, OperationDto>()
                .ForMember(c => c.CategoryName, opt => opt.MapFrom(o =>
                    categoriesRepo.FirstOrDefault(c => c.Id == o.CategoryId).CategoryName));
        }
    }
}
