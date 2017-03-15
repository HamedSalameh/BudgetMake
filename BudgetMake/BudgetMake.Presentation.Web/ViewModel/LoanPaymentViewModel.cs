using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class LoanPaymentViewModel : BudgetItemViewModelBase
    {
        [Display(Name = "ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public int LoanPaymentId { get; set; }

        public int MonthlyBudgetId { get; set; }

        [Display(Name = "BudgetItem_PaymentDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual DateTime PaymentDate { get; set; }

        public virtual int LoanId { get; set; }
    }

    
}