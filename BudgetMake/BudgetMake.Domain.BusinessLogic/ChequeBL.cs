using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class ChequeBL : BaseBL<Cheque>, IChequeBL
    {
        public ChequeBL(IChequeRepo ChequeRepo, ILocalLogger Log) : base(ChequeRepo, Log)
        {

        }
    }
}
