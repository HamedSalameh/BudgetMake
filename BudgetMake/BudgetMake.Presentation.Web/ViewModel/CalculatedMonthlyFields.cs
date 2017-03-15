using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class CalculatedMonthlyFields : CalculatedFieldsBase
    {
        public CalculatedMonthlyFields()
        {
            IsUsageOverBudget = false;
            AmountUsagePercentage = 0;

            AllocatedBudget = 0;
            UnallocatedBudget = 0;
            UsedBudget = 0;
        }
        // Booleans
        public bool IsOverAllocation { get; set; }

        public bool IsUsageOverBaseBudget { get; set; }

        [Display(Name = "MonthlyPlan_TotalIncome", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual double TotalIncome { get; set; }

        [Display(Name = "MonthlyPlan_AllocatedBudget", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual double AllocatedBudget { get; set; }

        [Display(Name = "MonthlyPlan_UnallocatedBudget", ResourceType = typeof(Shared.Common.Resources.General))]
        public virtual double UnallocatedBudget { get; set; }

        /// <summary>
        /// Indicates the total sum of used amount in all child budget items (exluding incomes)
        /// </summary>
        [Display(Name = "MonthlyPlan_UsedBudget", ResourceType = typeof(Shared.Common.Resources.General))]
        public double UsedBudget { get; set; }

        /// <summary>
        /// Indicated the sum of all used budget items amounts in comparison to sum of all allocated budget item amount (Actual Usage VS Allocations)
        /// </summary>
        [Display(Name = "MonthlyPlan_ValueOf_UsedAmountVSAllocatedAmount", ResourceType = typeof(Shared.Common.Resources.General))]
        public double ValueOf_UsedAmountVSAllocatedAmount { get; set; }

        /// <summary>
        /// Indicated the percentage of Used Amounts from Allocated Amount
        /// </summary>
        [Display(Name = "MonthlyPlan_PercentOf_UsedAmountVSAllocatedAmount", ResourceType = typeof(Shared.Common.Resources.General))]
        public double PercentOf_UsedAmountVSAllocatedAmount { get; set; }

        /// <summary>
        /// Income VS Allocated : Indicates the total income against the total expenses in the monthly plan
        /// </summary>
        [Display(Name = "MonthlyPlan_ValueOf_IncomeVsAllocatedExpenses", ResourceType = typeof(Shared.Common.Resources.General))]
        public double ValueOf_IncomeVsAllocatedExpenses { get; set; }

        /// <summary>
        /// Income VS Allocated : Indicates the total income percentage againt the expenses
        /// </summary>
        [Display(Name = "MonthlyPlan_PercentOf_IncomeVsAllocatedExpenses", ResourceType = typeof(Shared.Common.Resources.General))]
        public double PercentOf_IncomeVsAllocatedExpenses { get; set; }

        /// <summary>
        /// Allocated VS Income : Indicates the total expenses against the total income in the monthly plan 
        /// </summary>
        [Display(Name = "MonthlyPlan_ValueOf_AllocatedExpensesVSIncome", ResourceType = typeof(Shared.Common.Resources.General))]
        public double ValueOf_AllocatedExpensesVSIncome { get; set; }

        /// <summary>
        /// Allocated VS Income : Indicates the total expenses percentage from the total income
        /// </summary>
        [Display(Name = "MonthlyPlan_PercentOf_AllocatedExpensesVSIncome", ResourceType = typeof(Shared.Common.Resources.General))]
        public double PercentOf_AllocatedExpensesVSIncome { get; set; }

        /// <summary>
        /// Income VS Used
        /// </summary>
        [Display(Name = "MonthlyPlan_ValueOf_IncomeVsUsed", ResourceType = typeof(Shared.Common.Resources.General))]
        public double ValueOf_IncomeVsUsed { get; set; }

        /// <summary>
        /// Income VS Used
        /// </summary>
        [Display(Name = "MonthlyPlan_PercentOf_IncomeVsUsed", ResourceType = typeof(Shared.Common.Resources.General))]
        public double PercentOf_IncomeVsUsed { get; set; }

        /// <summary>
        /// Used VS Income
        /// </summary>
        [Display(Name = "MonthlyPlan_ValueOf_UsedVsIncome", ResourceType = typeof(Shared.Common.Resources.General))]
        public double ValueOf_UsedVsIncome { get; set; }

        /// <summary>
        /// Used VS Incomed
        /// </summary>
        [Display(Name = "MonthlyPlan_PercentOf_UsedVsIncome", ResourceType = typeof(Shared.Common.Resources.General))]
        public double PercentOf_UsedVsIncome { get; set; }

        // Predictions
        /// <summary>
        /// <para>Indicates the predicted balane value at end of month</para>
        /// <para>Only calcualted if OpenBalance was provided</para>
        /// </summary>
        [Display(Name = "MonthlyPlan_PredictedEndOfMonthBalance", ResourceType = typeof(Shared.Common.Resources.General))]
        public double ValueOf_PredictedEndOfMonthBalance { get; set; }
    }
}