using MediatR;

namespace FinanceAccounting.Logic.Commands.Create.IncomeCategory
{
    public class CreateIncomeCategoryCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public string CategoryName { get; set; }
    }
}
