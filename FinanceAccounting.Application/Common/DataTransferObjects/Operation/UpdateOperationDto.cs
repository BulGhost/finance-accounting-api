using System;

namespace FinanceAccounting.Application.Common.DataTransferObjects.Operation
{
    public class UpdateOperationDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public decimal Sum { get; set; }
        public string Details { get; set; }
    }
}
