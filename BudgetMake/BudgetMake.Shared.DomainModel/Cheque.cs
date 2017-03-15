using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Shared.DomainModel
{
    public class Cheque : BudgetItemBase
    {
        [Required]
        public virtual DateTime PaymentDate { get; set; }

        [Required]
        public virtual string Payee { get; set; }
    }
}
