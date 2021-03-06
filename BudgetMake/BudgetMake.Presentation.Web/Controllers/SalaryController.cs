﻿using BudgetMake.Presentation.Web.Extentions;
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
            PartialViewNameFor_DeleteItem = "DeleteSalaryItem";
            PartialViewNameFor_CreateItem = "CreateSalaryItem";
        }

        protected override SalaryViewModel MapToViewModel(Salary model)
        {
            return model.MapToViewModel();
        }

        protected override IList<SalaryViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            return application.GetEntities<Salary>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToSalaryViewModelList();
        }

        protected override Salary MapToModel(SalaryViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }
    }
}