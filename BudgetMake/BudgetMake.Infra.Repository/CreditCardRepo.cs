using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class CreditCardRepo : GenericRepository<CreditCard, BudgetMakeDBContext>, ICreditCardRepo
    {
        public CreditCardRepo(ILocalLogger Log)
        {
            _log = Log;
            _log.SetType(typeof(CreditCardRepo));
        }
    }
}
