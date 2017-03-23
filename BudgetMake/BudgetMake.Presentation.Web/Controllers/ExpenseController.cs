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

        protected override BaseResult UpdateModel(Expense model)
        {
            return application.UpdateBudgetItem<Expense>(model);
        }

        protected override BaseResult CreateModel(Expense model)
        {
            return application.CreateBudgetItem<Expense>(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteExpense(int? budgetItemId)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;
            if (budgetItemId != null)
            {
                try
                {
                    Expense model = application.GetById<Expense>(budgetItemId.Value);
                    if (model != null)
                    {
                        result = application.DeleteBudget(model);
                    }
                    else
                    {
                        results.Add(new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                        {
                            Message = Shared.Common.Resources.Errors.Http_404_NotFound_404,
                            Value = HttpStatusCode.NotFound
                        });
                    }
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    results.Add(new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                    {
                        Message = Ex.Message,
                        Value = HttpStatusCode.InternalServerError
                    });
                }
            }

            if (result != null)
            {
                results.Add(result);
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }
    }
}