using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;
using System;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class SalaryController : BaseController<Salary, SalaryViewModel>
    {
        public SalaryController(IApplication Application, ILocalLogger Logger) : base(Application, Logger)
        {
            partialViewNameFor_ItemsList = "Salaries";
            partialViewNameFor_EditItem = "EditSalaryItem";
        }

        public override SalaryViewModel GetViewModel(Salary model)
        {
            return model.MapToViewModel();
        }

        public override IList<SalaryViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            return application.GetEntities<Salary>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToSalaryViewModelList();
        }
    }
}