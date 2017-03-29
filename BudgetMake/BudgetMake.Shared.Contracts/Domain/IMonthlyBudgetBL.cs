using BudgetMake.Shared.DomainModel;
using GeneralServices;

namespace BudgetMake.Shared.Contracts.Domain
{
    public interface IMonthlyBudgetBL : ICoreActions<MonthlyBudget>
    {
        BaseResult updateMonthlyPlanPerBudgetItemUpdates(dynamic budgetItem, int budgetItemId);
    }
}
