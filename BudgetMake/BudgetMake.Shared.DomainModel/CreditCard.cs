using System;
using System.ComponentModel.DataAnnotations;
using static GeneralServices.Enums;

namespace BudgetMake.Shared.DomainModel
{
    public class CreditCard : BudgetItemBase
    {
        [Required]
        public virtual DateTime PaymentDate { get; set; }

        [Required]
        public virtual CreditCardType CardType { get; set; }
    }
}
