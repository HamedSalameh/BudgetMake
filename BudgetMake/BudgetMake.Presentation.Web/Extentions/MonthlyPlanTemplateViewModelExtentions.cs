using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class MonthlyPlanTemplateViewModelExtentions
    {
        public static MonthlyPlanTemplate MapToModel(this MonthlyPlanTemplateViewModel model)
        {
            MonthlyPlanTemplate template = null;

            if (model != null)
            {
                template = new MonthlyPlanTemplate
                {
                    Id = model.MonthlyPlanId,
                    TemplateName = model.TemplateName,
                    Template = model.Template,
                };
            }
            return template;
        }
    }
}