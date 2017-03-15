using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class IncomeBL : BaseBL<Income>, IIncomeBL
    {
        public IncomeBL(IIncomeRepo IncomeRepo, ILocalLogger Log) : base(IncomeRepo, Log)
        {
        }
    }
}
