using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;
using System;
using GeneralServices;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class SalaryController : BaseController<Salary, SalaryViewModel>
    {
        public SalaryController(IApplication Application, ILocalLogger Logger) : base(Application, Logger)
        {
            PartialViewNameFor_ItemsList = "Salaries";
            PartialViewNameFor_EditItem = "EditSalaryItem";
        }

        public override SalaryViewModel GetViewModel(Salary model)
        {
            return model.MapToViewModel();
        }

        public override IList<SalaryViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            return application.GetEntities<Salary>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToSalaryViewModelList();
        }

        public override Salary GetModel(SalaryViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }

        public override BaseResult UpdateModel(Salary model)
        {
            throw new NotImplementedException();
        }
    }
}