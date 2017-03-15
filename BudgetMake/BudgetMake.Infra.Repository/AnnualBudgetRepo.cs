using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class AnnualBudgetRepo : GenericRepository<AnnualBudget, BudgetMakeDBContext>, IAnnualBudgetRepo
    {
        public AnnualBudgetRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(AnnualBudgetRepo));
        }
    }
}
