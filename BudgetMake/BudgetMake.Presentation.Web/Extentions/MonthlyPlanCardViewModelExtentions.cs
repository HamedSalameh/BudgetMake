using BudgetMake.Presentation.Web.Helpers;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class MonthlyPlanCardViewModelExtentions
    {
        public static MonthlyPlanViewModel MapToViewModel(this MonthlyBudget monthyBudget)
        {
            if (monthyBudget != null)
            {
                return new MonthlyPlanViewModel
                {
                    AnnualBudgetId = monthyBudget.AnnualBudgetId,
                    BudgetForAllocation = monthyBudget.BaseBudget,
                    MonthlyBudgetId = monthyBudget.Id,
                    MonthName = monthyBudget.MonthName,
                    Comments = monthyBudget.Comments,
                    CreationDate = monthyBudget.CreationDate,
                    LastModified = monthyBudget.LastModifited,
                    CalculatedFields = CalculatedFieldsHelper.CreateCalculatedFieldsObject(monthyBudget)
                };
            }
            else
            {
                return null;
            }
        }

        public static IList<MonthlyPlanViewModel> Map(this IList<MonthlyBudget> monthlyBudgets)
        {
            IList<MonthlyPlanViewModel> viewModels = new List<MonthlyPlanViewModel>();

            if (monthlyBudgets != null && monthlyBudgets.Count > 0)
            {
                foreach (MonthlyBudget mb in monthlyBudgets)
                {
                    MonthlyPlanViewModel model = mb.MapToViewModel();
                    if (model != null)
                    {
                        viewModels.Add(model);
                    }
                }
            }

            return viewModels;
        }

        public static MonthlyBudget MapToModel(this MonthlyPlanViewModel monthlyPlanVM)
        {
            MonthlyBudget budget = null;
            if (monthlyPlanVM != null)
            {
                budget = new MonthlyBudget()
                {
                    BaseBudget = monthlyPlanVM.BudgetForAllocation,
                    Comments = monthlyPlanVM.Comments,
                    MonthName = monthlyPlanVM.MonthName,
                    Id = monthlyPlanVM.MonthlyBudgetId,
                    AnnualBudgetId = monthlyPlanVM.AnnualBudgetId,
                    LastModifited = monthlyPlanVM.LastModified,
                    CreationDate = monthlyPlanVM.CreationDate
                };
            }
            return budget;
        }
    }
}