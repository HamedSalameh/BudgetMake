using BudgetMake.Presentation.Web.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class BudgetItemViewModelBase
    {
        [Display(Name = "CreationDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime CreationDate { get; set; }

        [Display(Name = "LastModified", ResourceType = typeof(Shared.Common.Resources.General))]
        public DateTime LastModified { get; set; }

        [Display(Name = "BudgetItem_Description", ResourceType = typeof(Shared.Common.Resources.General))]
        public string Description { get; set; }

        [Display(Name = "BudgetItem_Amount", ResourceType = typeof(Shared.Common.Resources.General))]
        [PositiveNumber("General_NegativeValue", "General_NaN", typeof(Shared.Common.Resources.Errors))]
        public double Amount { get; set; }

        [Display(Name = "BudgetItem_AmountUsed", ResourceType = typeof(Shared.Common.Resources.General))]
        [PositiveNumber("General_NegativeValue", "General_NaN", typeof(Shared.Common.Resources.Errors))]
        public double AmountUsed { get; set; }

        [Display(Name = "BudgetItem_Comments", ResourceType = typeof(Shared.Common.Resources.General))]
        public string Comments { get; set; }

        public CalculatedBudgetItemFields CalculatedFields { get; set; }
    }
}