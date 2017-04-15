using System;
using System.ComponentModel.DataAnnotations;
using static GeneralServices.Enums;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class CreditCardViewModel : BudgetItemViewModelBase
    {
        [Display(Name = "ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public int CreditCardId { get; set; }

        [Display(Name = "BudgetItem_PaymentDate", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual DateTime PaymentDate { get; set; }

        [Display(Name = "BudgetItem_CreditCardType", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual CreditCardType CardType { get; set; }
    }

    
}