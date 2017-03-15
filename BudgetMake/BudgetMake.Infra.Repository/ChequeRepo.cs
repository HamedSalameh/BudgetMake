using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class ChequeRepo : GenericRepository<Cheque, BudgetMakeDBContext>, IChequeRepo
    {
        public ChequeRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(ChequeRepo));
        }
    }
}
