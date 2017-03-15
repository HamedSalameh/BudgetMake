using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Shared.DomainModel
{
    public class AnnualBudget : Entity
    {
        [MaxLength(4)]
        public virtual string AnnualBudgetName { get; set; }

        [MaxLength(50)]
        public virtual string Description { get; set; }

        public virtual ICollection<MonthlyBudget> MonthlyBudgets { get; set; }
    }
}
