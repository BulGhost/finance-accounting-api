using System;

namespace FinanceAccounting.Application.Common.DataTransferObjects.OperationDto
{
    public class CreateOperationDto
    {
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public decimal Sum { get; set; }
        public string Details { get; set; }
    }
}
