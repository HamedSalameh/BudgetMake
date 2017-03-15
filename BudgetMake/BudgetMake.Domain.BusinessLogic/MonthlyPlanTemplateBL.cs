using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class MonthlyPlanTemplateBL : BaseBL<MonthlyPlanTemplate>, IMonthlyPlanTemplateBL
    {
        public MonthlyPlanTemplateBL(IMonthlyPlanTemplateRepo MonthlyPlanTemplateRepo, ILocalLogger Log) : base(MonthlyPlanTemplateRepo, Log)
        {
            
        }
    }
}
