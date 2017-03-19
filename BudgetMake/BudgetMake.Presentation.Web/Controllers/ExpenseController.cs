using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.Helpers;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using GeneralServices;
using GeneralServices.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using static GeneralServices.Enums;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class ExpenseController : BaseController<Expense, ExpenseViewModel>
    {
        public ExpenseController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {
            PartialViewNameFor_ItemsList = "Expenses";
            PartialViewNameFor_EditItem = "EditExpenseItem";
            PartialViewNameFor_DeleteItem = "DeleteExpenseItem";
        }

        public override IList<ExpenseViewModel> GetViewModelsList(int MonthlyPlanId)
        {
            return application.GetEntities<Expense>(e => e.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
        }

        public override ExpenseViewModel GetViewModel(Expense model)
        {
            return model.MapToViewModel();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditExpenseItem(ExpenseViewModel expenseViewModel)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;
            if (expenseViewModel != null)
            {
                if (ModelState.IsValid)
                {
                    Expense budget = expenseViewModel.MapToModel();
                    if (budget != null)
                    {
                        try
                        {
                            result = application.UpdateBudget(budget);
                        }
                        catch (Exception Ex)
                        {
                            HandleException(Ex);
                            result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                            {
                                Message = Ex.Message,
                                Value = HttpStatusCode.InternalServerError
                            };
                        }
                    }
                    else
                    {
                        result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                        {
                            Message = Shared.Common.Resources.Errors.General_UnableToMapToModel,
                            Value = HttpStatusCode.InternalServerError
                        };
                    }
                }
                else
                {
                    results = BaseResultHelper.GetModelErrors(ModelState);
                }
            }
            else
            {
                result = new ValidationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = Shared.Common.Resources.Errors.Http_400_BadRequest,
                    Value = HttpStatusCode.BadRequest
                };
            }

            if (result != null)
            {
                results.Add(result);
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult QuickEditExpenseItem(int? budgetItemId, string PropName, string PropValue)
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
                        if (!string.IsNullOrEmpty(PropName))
                        {
                            bool updateResult = Reflection.UpdateObjectProperty(model, PropName, PropValue);
                            if (updateResult)
                            {
                                ExpenseViewModel viewModel = model.MapToViewModel();
                                TryValidateModel(viewModel);
                                if (ModelState.IsValid)
                                {
                                    try
                                    {
                                        result = application.UpdateBudget(model);
                                    }
                                    catch (Exception Ex)
                                    {
                                        HandleException(Ex);
                                        result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                                        {
                                            Message = Ex.Message,
                                            Value = HttpStatusCode.InternalServerError
                                        };
                                    }
                                }
                                else
                                {
                                    results = BaseResultHelper.GetModelErrors(ModelState);
                                }
                            }
                            else
                            {
                                result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                                {
                                    Message = "Unable to update value",
                                    Value = PropValue
                                };
                            }
                        }
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

        [HttpGet]
        public PartialViewResult CreateExpenseItem(string MonthlyPlanId)
        {
            int monthlyPlanId = 0;
            int.TryParse(MonthlyPlanId, out monthlyPlanId);

            if (monthlyPlanId != 0)
            {
                ExpenseViewModel viewModel = new ExpenseViewModel();
                viewModel.MonthlyBudgetId = monthlyPlanId;
                return PartialView(viewModel);
            }
            else
            {
                return PartialView("_badRequest");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateExpenseItem(ExpenseViewModel expenseViewModel)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;
            if (expenseViewModel != null)
            {
                if (ModelState.IsValid)
                {
                    Expense budgetItem = expenseViewModel.MapToModel();
                    if (budgetItem != null)
                    {
                        try
                        {
                            result = application.CreateBudget(budgetItem);
                        }
                        catch (Exception Ex)
                        {
                            HandleException(Ex);
                            result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                            {
                                Message = Ex.Message,
                                Value = HttpStatusCode.InternalServerError
                            };
                        }
                    }
                    else
                    {
                        result = new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                        {
                            Message = Shared.Common.Resources.Errors.General_UnableToMapToModel,
                            Value = HttpStatusCode.InternalServerError
                        };
                    }
                }
                else
                {
                    results = BaseResultHelper.GetModelErrors(ModelState);
                }
            }
            else
            {
                result = new ValidationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = Shared.Common.Resources.Errors.Http_400_BadRequest,
                    Value = HttpStatusCode.BadRequest
                };
            }

            if (result != null)
            {
                results.Add(result);
            }

            return Json(results, JsonRequestBehavior.AllowGet);
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