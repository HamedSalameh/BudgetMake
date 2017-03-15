using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace BudgetMake.Shared.DomainModel
{
    public class BudgetItemBase : Entity
    {
        [MaxLength(50)]
        public virtual string Description { get; set; }

        [Required]
        public virtual double Amount { get; set; }

        public virtual double AmountUsed { get; set; }

        [MaxLength(250)]
        public virtual string Comments { get; set; }

        // FK
        [Required]
        public virtual int MonthlyBudgetId { get; set; }

        [ForeignKey("MonthlyBudgetId")]
        [ScriptIgnore]
        public MonthlyBudget MonthlyBudget { get; set; }
    }
}
