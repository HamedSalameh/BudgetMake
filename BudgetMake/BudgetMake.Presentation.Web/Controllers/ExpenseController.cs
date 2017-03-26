using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.Helpers;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using GeneralServices;
using GeneralServices.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using static GeneralServices.Enums;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class ExpenseController : BudgetItemBaseController<Expense, ExpenseViewModel>
    {
        public ExpenseController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {
            PartialViewNameFor_ItemsList = "Expenses";
            PartialViewNameFor_CreateItem = "CreateExpenseItem";
            PartialViewNameFor_EditItem = "EditExpenseItem";
            PartialViewNameFor_DeleteItem = "DeleteExpenseItem";
        }

        protected override IList<ExpenseViewModel> GetViewModelsList(int MonthlyPlanId)
        {
            return application.GetEntities<Expense>(e => e.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
        }

        protected override ExpenseViewModel GetViewModel(Expense model)
        {
            return model.MapToViewModel();
        }

        protected override Expense GetModel(ExpenseViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }
    }
}