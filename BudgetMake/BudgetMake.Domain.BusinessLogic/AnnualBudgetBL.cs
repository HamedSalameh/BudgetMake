using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class AnnualBudgetBL : BaseBL<AnnualBudget>, IAnnualBudgetBL
    {
        public AnnualBudgetBL(IAnnualBudgetRepo AnnualBudgetRepo, ILocalLogger Log) : base(AnnualBudgetRepo, Log)
        {
        }
    }
}
