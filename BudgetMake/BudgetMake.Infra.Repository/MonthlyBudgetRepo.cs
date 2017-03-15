using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class MonthlyBudgetRepo : GenericRepository<MonthlyBudget, BudgetMakeDBContext>, IMonthlyBudgetRepo
    {
        public MonthlyBudgetRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(MonthlyBudgetRepo));
        }
    }
}
