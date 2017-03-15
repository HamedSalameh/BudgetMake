using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class IncomeRepo : GenericRepository<Income, BudgetMakeDBContext>, IIncomeRepo
    {
        public IncomeRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(IncomeRepo));
        }
    }
}
