using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class ExpenseBL : BaseBL<Expense>, IExpenseBL
    {
        public ExpenseBL(IExpenseRepo ExpenseRepo, ILocalLogger Log) : base(ExpenseRepo, Log)
        {
        }
    }
}
