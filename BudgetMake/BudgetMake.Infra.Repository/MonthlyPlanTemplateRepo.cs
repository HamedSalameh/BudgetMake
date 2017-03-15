using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Infra.Repository
{
    public class MonthlyPlanTemplateRepo : GenericRepository<MonthlyPlanTemplate, BudgetMakeDBContext>, IMonthlyPlanTemplateRepo
    {
    }
}
