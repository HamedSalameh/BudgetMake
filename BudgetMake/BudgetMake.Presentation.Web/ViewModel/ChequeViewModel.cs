using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class ChequeViewModel : BudgetItemViewModelBase
    {
        [Display(Name = "ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public int ChequeId { get; set; }

        [Display(Name = "BudgetItem_PaymentDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual DateTime PaymentDate { get; set; }

        [Display(Name = "BudgetItem_Payee", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual string Payee { get; set; }
    }
}