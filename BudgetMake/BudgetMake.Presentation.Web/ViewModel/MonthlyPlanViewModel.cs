using BudgetMake.Presentation.Web.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using static GeneralServices.Enums;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class MonthlyPlanViewModel
    {
        [Display(Name = "ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual int MonthlyBudgetId { get; set; }

        [Display(Name = "CreationDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "LastModified", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime LastModified { get; set; }

        [Display(Name = "MonthlyPlan_MonthName", ResourceType = typeof(Shared.Common.Resources.General))]
        [Required(AllowEmptyStrings = false,
            ErrorMessageResourceName = "MonthlyPlan_InvalidName",
            ErrorMessageResourceType = typeof(Shared.Common.Resources.Errors))]
        public virtual MonthNames MonthName { get; set; }

        [Display(Name = "MonthlyPlan_OpeningBalance", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual double OpeningBalance { get; set; }

        [Display(Name = "MonthlyPlan_BudgetForAllocation", ResourceType = typeof(Shared.Common.Resources.General))]
        [PositiveNumber("General_NegativeValue", "General_NaN", typeof(Shared.Common.Resources.Errors))]
        public virtual double BudgetForAllocation { get; set; }

        [Display(Name = "MonthlyPlan_Comments", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual string Comments { get; set; }

        public int AnnualBudgetId { get; set; }

        // Calculated fields
        public CalculatedMonthlyFields CalculatedFields { get; set; }
    }
}