using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Shared.DomainModel
{
    public class MonthlyPlanTemplate : Entity
    {
        [Required]
        public virtual string TemplateName { get; set; }

        [Required]
        public virtual string Template { get; set; }
    }

    public class MonthlyPlanTemplateInfo
    {
        [Required]
        public virtual int PlanId { get; set; }

        [Required]
        public virtual string TemplateName { get; set; }
    }
}
