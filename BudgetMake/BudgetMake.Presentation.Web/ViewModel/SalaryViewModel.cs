using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class SalaryViewModel : BudgetItemViewModelBase
    {
        [Display(Name = "ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public int SalaryId { get; set; }

        public int MonthlyBudgetId { get; set; }

        [Display(Name = "BudgetItem_PaymentDate", ResourceType = typeof(Shared.Common.Resources.General))]
        [Required]
        public virtual DateTime PaymentDate { get; set; }
    }
}