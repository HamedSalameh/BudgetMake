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
    public class SalaryController : BudgetItemBaseController<Salary, SalaryViewModel>
    {
        public SalaryController(IApplication Application, ILocalLogger Logger) : base(Application, Logger)
        {
            PartialViewNameFor_ItemsList = "Salaries";
            PartialViewNameFor_EditItem = "EditSalaryItem";
        }

        protected override SalaryViewModel GetViewModel(Salary model)
        {
            return model.MapToViewModel();
        }

        protected override IList<SalaryViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            return application.GetEntities<Salary>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToSalaryViewModelList();
        }

        protected override Salary GetModel(SalaryViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }

        protected override BaseResult UpdateModel(Salary model)
        {
            return application.UpdateBudgetItem<Salary>(model);
        }

        protected override BaseResult CreateModel(Salary model)
        {
            return application.CreateBudgetItem<Salary>(model);
        }
    }
}