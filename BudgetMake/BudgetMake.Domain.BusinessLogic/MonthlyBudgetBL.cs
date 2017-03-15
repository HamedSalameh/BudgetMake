using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class MonthlyBudgetBL : BaseBL<MonthlyBudget>, IMonthlyBudgetBL
    {
        public MonthlyBudgetBL(IMonthlyBudgetRepo MonthlyBudgetRepo, ILocalLogger Log) : base(MonthlyBudgetRepo, Log)
        {
        }
    }
}
