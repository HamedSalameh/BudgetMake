using BudgetMake.Presentation.Web.Helpers;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
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
    public abstract class BudgetItemBaseController<Model, ViewModel> : BaseController<Model, ViewModel> where Model : class
    {
        private string partialViewNameFor_ItemsList { get; set; }
        private string partialViewNameFor_CreateItem { get; set; }
        private string partialViewNameFor_EditItem { get; set; }
        private string partialViewNameFor_DeleteItem { get; set; }

        public string PartialViewNameFor_ItemsList
        {
            get
            {
                return partialViewNameFor_ItemsList;
            }
            set
            {
                partialViewNameFor_ItemsList = value;
            }
        }
        public string PartialViewNameFor_CreateItem
        {
            get
            {
                return partialViewNameFor_CreateItem;
            }
            set
            {
                partialViewNameFor_CreateItem = value;
            }
        }
        public string PartialViewNameFor_EditItem
        {
            get
            {
                return partialViewNameFor_EditItem;
            }
            set
            {
                partialViewNameFor_EditItem = value;
            }
        }
        public string PartialViewNameFor_DeleteItem
        {
            get
            {
                return partialViewNameFor_DeleteItem;
            }
            set
            {
                partialViewNameFor_DeleteItem = value;
            }
        }

        public BudgetItemBaseController(IApplication ApplicaionLayer, ILocalLogger Log) : base(ApplicaionLayer, Log)
        {
            
        }

        [HttpGet]
        public PartialViewResult GetBudgetItemsList(int MonthlyPlanId = 0)
        {
            List<BaseResult> results = new List<BaseResult>();
            IList<ViewModel> Expenses = new List<ViewModel>();
            if (MonthlyPlanId != 0)
            {
                try
                {
                    Expenses = GetViewModelsList(MonthlyPlanId);
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    results.Add(
                        new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                        {
                            Message = "Unable to get monthly expenses",
                            Value = Ex.Message
                        }
                        );
                }
            }
            else
            {
                results.Add(
                            new OperationResult(ResultStatus.Exception, Reflection.GetCurrentMethodName())
                            {
                                Message = Shared.Common.Resources.Errors.Http_404_NotFound_404,
                                Value = HttpStatusCode.NotFound
                            }
                        );

            }
            TempData[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(results);
            return PartialView(partialViewNameFor_ItemsList, Expenses);
        }

        [HttpGet]
        public PartialViewResult CreateBudgetItem(string MonthlyPlanId)
        {
            int monthlyPlanId = 0;
            int.TryParse(MonthlyPlanId, out monthlyPlanId);

            if (monthlyPlanId != 0)
            {
                ExpenseViewModel viewModel = new ExpenseViewModel();
                viewModel.MonthlyBudgetId = monthlyPlanId;
                return PartialView(PartialViewNameFor_CreateItem, viewModel);
            }
            else
            {
                return PartialView("_badRequest");
            }
        }

        [HttpGet]
        public PartialViewResult EditBudgetItem(int? budgetItemId)
        {
            List<BaseResult> results = new List<BaseResult>();
            ViewModel viewModel = default(ViewModel);

            if (budgetItemId != null)
            {
                try
                {
                    Model model = application.GetById<Model>(budgetItemId.Value);
                    if (model != null)
                    {
                        viewModel = GetViewModel(model);
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
            else
            {
                results.Add(new ValidationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = Shared.Common.Resources.Errors.Http_400_BadRequest,
                    Value = HttpStatusCode.BadRequest
                });
            }

            TempData[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(results);
            return PartialView(partialViewNameFor_EditItem, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual JsonResult EditBudgetItem(ViewModel viewModel)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;
            if (viewModel != null)
            {
                if (ModelState.IsValid)
                {
                    Model budget = GetModel(viewModel);
                    if (budget != null)
                    {
                        try
                        {
                            result = UpdateModel(budget);
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
                        result = new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
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
        public JsonResult QuickEditBudgetItem(int? budgetItemId, string PropName, string PropValue)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;
            if (budgetItemId != null)
            {
                try
                {
                    Model model = application.GetById<Model>(budgetItemId.Value);
                    if (model != null)
                    {
                        if (!string.IsNullOrEmpty(PropName))
                        {
                            bool updateResult = Reflection.UpdateObjectProperty(model, PropName, PropValue);
                            if (updateResult)
                            {
                                ViewModel viewModel = GetViewModel(model);
                                TryValidateModel(viewModel);
                                if (ModelState.IsValid)
                                {
                                    try
                                    {
                                        result = UpdateModel(model);
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
        public PartialViewResult DeleteBudgetItem(int? budgetItemId = 0)
        {
            List<BaseResult> results = new List<BaseResult>();
            ViewModel viewModel = default(ViewModel);

            if (budgetItemId != null)
            {
                try
                {
                    Model model = application.GetById<Model>(budgetItemId.Value);
                    if (model != null)
                    {
                        viewModel = GetViewModel(model);
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
            else
            {
                results.Add(new ValidationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = Shared.Common.Resources.Errors.Http_400_BadRequest,
                    Value = HttpStatusCode.BadRequest
                });
            }

            TempData[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(results);
            return PartialView(partialViewNameFor_DeleteItem, viewModel);
        }

        [HttpGet]
        public PartialViewResult EntityHistory(int? budgetItemId)
        {
            List<BaseResult> results = new List<BaseResult>();
            List<HistoryLogViewModel> viewModel = new List<HistoryLogViewModel>();

            if (budgetItemId != null)
            {
                // try get budget item history using HistorySerivce
                viewModel = GeneralServices.Services.HistoryService.Instance.GetEntityHistory(budgetItemId.Value).MapHistoryLog();
            }
            else
            {
                results.Add(new ValidationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = Shared.Common.Resources.Errors.Http_400_BadRequest,
                    Value = HttpStatusCode.BadRequest
                });
            }

            TempData[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(results);
            return PartialView("_EntityHistory", viewModel);
        }

        [HttpGet]
        public PartialViewResult HistoryEventDetails(int? HistoryLogId)
        {
            List<BaseResult> results = new List<BaseResult>();
            List<EntityPropertyChangeViewModel> viewModel = new List<EntityPropertyChangeViewModel>();
            BaseResult result = null;

            if (HistoryLogId != null)
            {
                viewModel = GeneralServices.Services.HistoryService.Instance.GetEntityDetailedHistory(HistoryLogId.Value).MapEPCList();
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

            TempData[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(results);
            return PartialView("_EntityHistoryDetails", viewModel);
        }
    }
}