using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class MonthlyPlanTemplateViewModel
    {
        public virtual int MonthlyPlanId { get; set; }

        [Display(Name = "MonthlyPlanTemplate_TemplateName", ResourceType = typeof(Shared.Common.Resources.General))]
        [Required(AllowEmptyStrings = false, 
            ErrorMessageResourceName = "MonthlyPlanTemplate_InvalidName", 
            ErrorMessageResourceType = typeof(Shared.Common.Resources.Errors))]
        public virtual string TemplateName { get; set; }

        [Display(Name = "MonthlyPlanTemplate_Comments", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual string Template { get; set; }
    }
}