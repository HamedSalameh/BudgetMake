using BudgetMake.Presentation.Web.Helpers;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class BudgetItemViewModelExtentions
    {
        public static Expense MapToModel(this ExpenseViewModel budgetItemVM)
        {
            Expense budget = null;
            if (budgetItemVM != null)
            {
                budget = new Expense
                {
                    Amount = budgetItemVM.Amount,
                    AmountUsed = budgetItemVM.AmountUsed,
                    Id = budgetItemVM.BudgetItemId,
                    Comments = budgetItemVM.Comments,
                    Description = budgetItemVM.Description,
                    MonthlyBudgetId = budgetItemVM.MonthlyBudgetId,
                    CreationDate = budgetItemVM.CreationDate,
                    LastModifited = budgetItemVM.LastModified
                };
            }
            return budget;
        }

        public static IList<ExpenseViewModel> MapToViewModelsList(this IList<Expense> budgetItems)
        {
            List<ExpenseViewModel> viewModels = new List<ExpenseViewModel>();
            if (budgetItems != null && budgetItems.Count > 0)
            {
                foreach (Expense budgetItem in budgetItems)
                {
                    ExpenseViewModel viewModel = budgetItem.MapToViewModel();
                    if (viewModel != null)
                    {
                        viewModels.Add(viewModel);
                    }
                }
            }
            return viewModels;
        }

        public static ExpenseViewModel MapToViewModel(this Expense budgetItem)
        {
            ExpenseViewModel viewModel = null;
            if (budgetItem != null)
            {
                viewModel = new ExpenseViewModel
                {
                    Amount = budgetItem.Amount,
                    AmountUsed = budgetItem.AmountUsed,
                    BudgetItemId = budgetItem.Id,
                    Comments = budgetItem.Comments,
                    Description = budgetItem.Description,
                    MonthlyBudgetId = budgetItem.MonthlyBudgetId,
                    CreationDate = budgetItem.CreationDate,
                    LastModified = budgetItem.LastModifited,
                    CalculatedFields = CalculatedFieldsHelper.CreateCalculatedFieldsObject(budgetItem)
                };
            }
            return viewModel;
        }
    }
}