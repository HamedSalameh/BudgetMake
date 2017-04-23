using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class SalaryViewModelExtentions
    {
        public static Salary MapToModel(this SalaryViewModel SalaryVM)
        {
            Salary salary = null;
            if (SalaryVM != null)
            {
                salary = new Salary
                {
                    Amount = SalaryVM.Amount,
                    Comments = SalaryVM.Comments,
                    Description = SalaryVM.Description,
                    Id = SalaryVM.SalaryId,
                    MonthlyBudgetId = SalaryVM.MonthlyBudgetId,
                    PaymentDate = SalaryVM.PaymentDate,
                    CreationDate = SalaryVM.CreationDate,
                    LastModifited = SalaryVM.LastModified
                };
            }
            return salary;
        }

        public static SalaryViewModel MapToViewModel(this Salary Salary)
        {
            SalaryViewModel SalaryVM = null;

            if (Salary != null)
            {
                SalaryVM = new SalaryViewModel
                {
                    Amount = Salary.Amount,
                    Comments = Salary.Comments,
                    Description = Salary.Description,
                    MonthlyBudgetId = Salary.MonthlyBudgetId,
                    PaymentDate = Salary.PaymentDate,
                    SalaryId = Salary.Id,
                    CreationDate = Salary.CreationDate,
                    LastModified = Salary.LastModifited
                };
            }

            return SalaryVM;
        }

        public static List<SalaryViewModel> MapToSalaryViewModelList(this IList<Salary> Salaries)
        {
            List<SalaryViewModel> SalariesVM = new List<SalaryViewModel>();

            if (Salaries != null && Salaries.Count > 0)
            {
                foreach (Salary salary in Salaries)
                {
                    SalaryViewModel salaryViewModel = salary.MapToViewModel();
                    if (salaryViewModel != null)
                    {
                        SalariesVM.Add(salaryViewModel);
                    }
                }

            }

            return SalariesVM;
        }
    }
}