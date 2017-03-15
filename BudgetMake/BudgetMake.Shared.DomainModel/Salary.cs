using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Shared.DomainModel
{
    public class Salary : BudgetItemBase
    {
        [Required]
        public virtual DateTime PaymentDate { get; set; }
    }
}
