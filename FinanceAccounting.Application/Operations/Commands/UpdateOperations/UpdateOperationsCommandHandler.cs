using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FinanceAccounting.Application.Common.DataTransferObjects;
using FinanceAccounting.Application.Common.DataTransferObjects.Operation;
using FinanceAccounting.Domain.Entities;
using FinanceAccounting.Domain.Exceptions;
using FinanceAccounting.Domain.Repository;
using MediatR;

namespace FinanceAccounting.Application.Operations.Commands.UpdateOperations
{
    public class UpdateOperationsCommandHandler : IRequestHandler<UpdateOperationsCommand, IEnumerable<OperationDto>>
    {
        private readonly IOperationRepo _repo;
        private readonly IMapper _mapper;

        public UpdateOperationsCommandHandler(IOperationRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OperationDto>> Handle(UpdateOperationsCommand request, CancellationToken cancellationToken)
        {
            var updatedOperations = new List<Operation>();
            foreach (UpdateOperationDto operationDto in request.Operations)
            {
                Operation operation = await _repo.FindAsync(operationDto.Id, cancellationToken);

                if (operation == null || operation.UserId != request.UserId)
                {
                    throw new OperationNotFoundException(operationDto.Id);
                }

                operation.Date = operationDto.Date;
                operation.Type = await _repo.GetOperationTypeByCategoryId(operationDto.CategoryId);
                operation.CategoryId = operationDto.CategoryId;
                operation.Sum = operationDto.Sum;
                operation.Details = operationDto.Details;

                await _repo.UpdateAsync(operation, true, cancellationToken);
                updatedOperations.Add(operation);
            }

            return _mapper.Map<IEnumerable<OperationDto>>(updatedOperations);
        }
    }
}
