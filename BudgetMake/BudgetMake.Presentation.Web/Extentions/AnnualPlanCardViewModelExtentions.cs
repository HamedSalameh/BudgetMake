using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class AnnualPlanCardViewModelExtentions
    {
        public static AnnualPlanViewModel MapToViewModel(this AnnualBudget annual)
        {
            if (annual != null)
            {
                return new AnnualPlanViewModel
                {
                    AnnualBudgetId = annual.Id,
                    CreationDate = annual.CreationDate,
                    LastModified = annual.LastModifited,
                    AnnualBudgetName = annual.AnnualBudgetName
                };
            }
            else
            {
                return null;
            }
        }

        public static IList<AnnualPlanViewModel> Map(this IList<AnnualBudget> annuals)
        {
            IList<AnnualPlanViewModel> viewModels = new List<AnnualPlanViewModel>();
            if (annuals != null && annuals.Count > 0)
            {
                foreach (AnnualBudget a in annuals)
                {
                    AnnualPlanViewModel avm = a.MapToViewModel();
                    if (avm != null)
                    {
                        viewModels.Add(avm);
                    }
                }
            }
            return viewModels;
        }

        public static AnnualBudget MapToModel(this AnnualPlanViewModel annualVM)
        {
            AnnualBudget budget = null;
            if (annualVM != null)
            {
                budget = new AnnualBudget()
                {
                    Id = annualVM.AnnualBudgetId,
                    CreationDate = annualVM.CreationDate,
                    LastModifited = annualVM.LastModified,
                    AnnualBudgetName = annualVM.AnnualBudgetName
                };
            }

            return budget;
        }
    }
}