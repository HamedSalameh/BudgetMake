using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class CalculatedFieldsBase
    {
        public CalculatedFieldsBase()
        {
            IsUsageOverBudget = false;
            AmountUsagePercentage = 0;
        }

        // Auto-calculated fields
        /// <summary>
        /// <para>In budget items : indicates is actual usage higher than allocated budget</para>
        /// <para>In monthly plan : indicates is actual usage higher than total income</para>
        /// </summary>
        [Display(Name = "BudgetItem_OverBudget", ResourceType = typeof(Shared.Common.Resources.General))]
        public bool IsUsageOverBudget;

        [Display(Name = "BudgetItem_UsagePercentage", ResourceType = typeof(Shared.Common.Resources.General))]
        public double AmountUsagePercentage;
    }
}