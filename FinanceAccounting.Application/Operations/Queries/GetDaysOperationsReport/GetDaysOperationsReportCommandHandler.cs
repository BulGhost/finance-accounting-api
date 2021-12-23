using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Domain.Repository;
using MediatR;

namespace FinanceAccounting.Application.Operations.Queries.GetDaysOperationsReport
{
    public class GetDaysOperationsReportCommandHandler : IRequestHandler<GetDaysOperationsReportCommand, IEnumerable<OperationDto>>
    {
        private readonly IOperationRepo _repo;
        private readonly IMapper _mapper;

        public GetDaysOperationsReportCommandHandler(IOperationRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OperationDto>> Handle(GetDaysOperationsReportCommand request, CancellationToken cancellationToken)
        {
            var operations = await _repo.GetUserOperationsOnDate(request.UserId, request.Date);

            return _mapper.Map<IEnumerable<OperationDto>>(operations);
        }
    }
}
