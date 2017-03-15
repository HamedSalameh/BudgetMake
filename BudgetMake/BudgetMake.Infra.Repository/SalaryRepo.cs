using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class SalaryRepo : GenericRepository<Salary, BudgetMakeDBContext>, ISalaryRepo
    {
        public SalaryRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(SalaryRepo));
        }
    }
}
