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
    public abstract class BaseController<Model, ViewModel> : Controller where Model : class
    {
        protected IApplication application { get; set; }
        protected static ILocalLogger _log { get; set; }

        protected string partialViewNameFor_ItemsList { get; set; }
        protected string partialViewNameFor_EditItem { get; set; }

        public abstract IList<ViewModel> GetViewModelsList(int MonthlyPlanId = 0);
        public abstract ViewModel GetViewModel(Model model);

        public BaseController(IApplication ApplicaionLayer, ILocalLogger Log)
        {
            // Setup
            application = ApplicaionLayer;
            _log = Log;
            // Init logger
            _log = Log.SetType(typeof(Model));
        }

        protected void handleException(Exception Ex)
        {
            ExceptionHelpers.handleException(Ex, _log);
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
                    handleException(Ex);
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
                    handleException(Ex);
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

        [HttpGet]
        public PartialViewResult DeleteBudgetItem(int? budgetItemId = 0)
        {
            return null;
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