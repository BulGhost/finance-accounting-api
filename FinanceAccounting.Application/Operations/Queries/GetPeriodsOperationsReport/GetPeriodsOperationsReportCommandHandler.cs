using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Domain.Repository;
using MediatR;

namespace FinanceAccounting.Application.Operations.Queries.GetPeriodsOperationsReport
{
    public class GetPeriodsOperationsReportCommandHandler : IRequestHandler<GetPeriodsOperationsReportCommand, IEnumerable<OperationDto>>
    {
        private readonly IOperationRepo _repo;
        private readonly IMapper _mapper;

        public GetPeriodsOperationsReportCommandHandler(IOperationRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OperationDto>> Handle(GetPeriodsOperationsReportCommand request, CancellationToken cancellationToken)
        {
            var operations = await _repo.GetUserOperationsOnDateRange(request.UserId, request.StartDate, request.FinalDate);

            return _mapper.Map<IEnumerable<OperationDto>>(operations);
        }
    }
}
