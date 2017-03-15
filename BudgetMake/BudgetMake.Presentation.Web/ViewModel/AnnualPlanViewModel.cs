using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class AnnualPlanViewModel
    {
        [Display(Name ="ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual int AnnualBudgetId { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "LastModified", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime LastModified { get; set; }

        [Display(Name = "AnnualPlan_Name", ResourceType = typeof(Shared.Common.Resources.General))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "AnnualPlan_InvalidName", ErrorMessageResourceType = typeof(Shared.Common.Resources.Errors))]
        public virtual string AnnualBudgetName { get; set; }
    }
}