using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Linq;

namespace BudgetMake.Presentation.Web.Helpers
{
    public static class CalculatedFieldsHelper
    {
        public static CalculatedBudgetItemFields CreateCalculatedFieldsObject(BudgetItemBase budgetItem)
        {
            CalculatedBudgetItemFields cf = null;
            if (budgetItem != null)
            {
                cf = new CalculatedBudgetItemFields();
                if (budgetItem.AmountUsed > 0)
                {
                    cf.IsUsageOverBudget = (budgetItem.Amount > 0) ? budgetItem.AmountUsed > budgetItem.Amount : true;
                    cf.AmountUsagePercentage = (budgetItem.Amount > 0) ? budgetItem.AmountUsed / budgetItem.Amount : budgetItem.AmountUsed;
                }
            }
            return cf;
        }

        public static CalculatedMonthlyFields CreateCalculatedFieldsObject(MonthlyBudget monthly)
        {
            CalculatedMonthlyFields cf = null;
            if (monthly != null)
            {
                cf = new CalculatedMonthlyFields();

                cf.TotalIncome = monthly.Salaries.Sum(s => s.Amount) + monthly.AdditionalIncome.Sum(a => a.Amount);

                cf.AllocatedBudget = monthly.Expenses.Sum(e => e.Amount)
                                                + monthly.Cheques.Sum(c => c.Amount)
                                                + monthly.CreditCards.Sum(c => c.Amount)
                                                + monthly.LoansPayments.Sum(l => l.Amount);

                cf.UnallocatedBudget = monthly.BaseBudget - cf.AllocatedBudget;

                cf.UsedBudget = monthly.Expenses.Sum(e => e.AmountUsed)
                                                + monthly.Cheques.Sum(c => c.AmountUsed)
                                                + monthly.CreditCards.Sum(c => c.AmountUsed)
                                                + monthly.LoansPayments.Sum(l => l.AmountUsed);

                // Used VS Allocated
                cf.ValueOf_UsedAmountVSAllocatedAmount = cf.UsedBudget - cf.AllocatedBudget;
                cf.PercentOf_UsedAmountVSAllocatedAmount = (cf.AllocatedBudget > 0) ?  cf.UsedBudget / cf.AllocatedBudget : -999999;

                // Income VS Allocated
                cf.ValueOf_IncomeVsAllocatedExpenses = cf.TotalIncome - cf.AllocatedBudget;
                cf.PercentOf_IncomeVsAllocatedExpenses = cf.TotalIncome / cf.AllocatedBudget;

                // Allocated VS Income
                cf.ValueOf_AllocatedExpensesVSIncome = cf.ValueOf_IncomeVsAllocatedExpenses * (-1);
                cf.PercentOf_AllocatedExpensesVSIncome = 1 / cf.PercentOf_IncomeVsAllocatedExpenses;

                // Income VS Used
                cf.ValueOf_IncomeVsUsed = cf.TotalIncome - cf.UsedBudget;
                cf.PercentOf_IncomeVsUsed = cf.TotalIncome / cf.UsedBudget;

                // Used VS Income
                cf.ValueOf_UsedVsIncome = cf.ValueOf_IncomeVsUsed * (-1);
                cf.PercentOf_UsedVsIncome = 1 / cf.PercentOf_IncomeVsUsed;

                cf.ValueOf_PredictedEndOfMonthBalance = monthly.OpeningBalance > 0 ? monthly.OpeningBalance + (cf.TotalIncome - cf.UsedBudget)  : 0;

                cf.IsOverAllocation = cf.AllocatedBudget > monthly.BaseBudget;
                cf.IsUsageOverBudget = cf.UsedBudget > cf.TotalIncome;
                cf.IsUsageOverBaseBudget = cf.UsedBudget > monthly.BaseBudget;

            }
            return cf;
        }
    }
}