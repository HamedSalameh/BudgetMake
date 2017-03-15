using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class ExpenseRepo : GenericRepository<Expense, BudgetMakeDBContext>, IExpenseRepo
    {
        public ExpenseRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(ExpenseRepo));
        }
    }
}
