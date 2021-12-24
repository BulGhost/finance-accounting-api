using System;
using AutoMapper;
using FinanceAccounting.Application.Common.Mappings;
using FinanceAccounting.Domain.Entities;

namespace FinanceAccounting.Application.Common.DataTransferObjects.Operation
{
    public class OperationDto : IMapFrom<Domain.Entities.Operation>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OperationType Type { get; set; }
        public string CategoryName { get; set; }
        public decimal Sum { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Operation, OperationDto>()
                .ForMember(dto => dto.CategoryName,
                    opt => opt.MapFrom<CategoryNameValueResolver>());
        }
    }
}
