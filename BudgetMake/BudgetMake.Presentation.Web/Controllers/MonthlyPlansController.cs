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
    public class MonthlyPlansController : BaseController<MonthlyBudget, MonthlyPlanViewModel>
    {
        public MonthlyPlansController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {
        }

        [HttpGet]
        public ActionResult GetMonthlyPlans(int AnnualPlanID = 0)
        {
            if (AnnualPlanID != 0)
            {
                IList<MonthlyPlanViewModel> monthlyPlans = null;

                try
                {
                    // Load plans
                    IList<MonthlyBudget> monthlyBudgets = application.GetEntities<MonthlyBudget>(m => m.AnnualBudgetId == AnnualPlanID,
                        mp => mp.Expenses,
                        mp => mp.Cheques,
                        mp => mp.CreditCards,
                        mp => mp.LoansPayments,
                        mp => mp.Salaries,
                        mp => mp.AdditionalIncome);

                    monthlyPlans = monthlyBudgets.Map();
                    TempData["AnnualPlan"] = AnnualPlanID;

                    // Load templates
                    IList<MonthlyPlanTemplate> monthlyTemplates = application.GetEntities<MonthlyPlanTemplate>();
                    ViewBag.MonthlyTemplates = new SelectList(monthlyTemplates, "MonthlyPlanTemplateId", "TemplateName");
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                return View("MonthlyPlans", monthlyPlans);
            }
            else
            {
                // in case we don't have the annual plan id, redirect to main
                return RedirectToAction("GetAnnualPlans", "AnnualBudget");
            }
        }

        [HttpGet]
        public PartialViewResult CreatePlan(int? AnnualPlanId)
        {
            if (AnnualPlanId != null && AnnualPlanId.Value != 0)
            {
                MonthlyPlanViewModel model = new MonthlyPlanViewModel();
                model.AnnualBudgetId = AnnualPlanId.Value;
                return PartialView("CreatePlan", model);
            }
            else
            {
                return PartialView("_badRequest");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlan(MonthlyPlanViewModel monthlyPlan)
        {
            if (monthlyPlan != null)
            {
                if (ModelState.IsValid)
                {
                    MonthlyBudget budget = monthlyPlan.MapToModel();
                    if (budget != null)
                    {
                        try
                        {
                            BaseResult result = application.CreateMonthlyBudget(budget);
                            if (result.Status != ResultStatus.Success)
                            {
                                ViewBag[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(result);
                                return View(monthlyPlan);
                            }
                            return RedirectToAction("GetMonthlyPlans", new { AnnualPlanID = monthlyPlan.AnnualBudgetId });
                        }
                        catch (Exception Ex)
                        {
                            HandleException(Ex);
                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                }
                else
                {
                    return View(monthlyPlan);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public PartialViewResult Edit(int MonthlyPlanId)
        {
            List<BaseResult> results = new List<BaseResult>();
            MonthlyPlanViewModel viewModel = null;

            if (MonthlyPlanId != 0)
            {
                try
                {
                    MonthlyBudget monthly = application.GetMonthlyBudget(MonthlyPlanId);
                    if (monthly != null)
                    {
                        viewModel = MapToViewModel(monthly);
                        ViewName = "EditMonthlyPlan";
                    }
                    else
                    {
                        results.Add(new OperationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                        {
                            Message = Shared.Common.Resources.Errors.Http_404_NotFound_404,
                            Value = HttpStatusCode.NotFound
                        });
                        ViewName = sharedView_PageOrResourceNotFound;
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
                    ViewName = sharedView_InternalServerError;
                }
            }
            else
            {
                results.Add(new ValidationResult(ResultStatus.Failure, Reflection.GetCurrentMethodName())
                {
                    Message = Shared.Common.Resources.Errors.Http_400_BadRequest,
                    Value = HttpStatusCode.BadRequest
                });
                ViewName = sharedView_BadRequest;
            }

            TempData[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(results);
            return PartialView(ViewName, viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(MonthlyPlanViewModel monthlyPlan)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;

            if (monthlyPlan != null)
            {
                if (ModelState.IsValid)
                {
                    MonthlyBudget monthlyBudget = monthlyPlan.MapToModel();
                    if (monthlyBudget != null)
                    {
                        try
                        {
                            result = application.UpdateMonthlyPlan(monthlyBudget);
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

        [HttpGet]
        public PartialViewResult Delete(int MonthlyPlanId = 0)
        {
            List<BaseResult> results = new List<BaseResult>();
            MonthlyPlanViewModel viewModel = null;

            if (MonthlyPlanId != 0)
            {
                MonthlyBudget model = null;
                try
                {
                    model = application.GetMonthlyBudget(MonthlyPlanId);
                    if (model != null)
                    {
                        viewModel = MapToViewModel(model);
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
            return PartialView("DeleteMonthlyPlan", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? monthlyPlanId)
        {
            if (monthlyPlanId != null)
            {
                if (ModelState.IsValid)
                {
                    MonthlyBudget monthlyBudget = application.GetMonthlyBudget(monthlyPlanId.Value);
                    if (monthlyBudget != null)
                    {
                        try
                        {
                            int annualBudgetPlanId = monthlyBudget.AnnualBudgetId;
                            BaseResult result = application.DeleteMonthlyBudget(monthlyBudget);
                            if (result.Status != ResultStatus.Success)
                            {
                                ViewBag[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(result);
                                return View();
                            }
                            return RedirectToAction("GetMonthlyPlans", new { AnnualPlanID = annualBudgetPlanId });
                        }
                        catch (Exception Ex)
                        {
                            HandleException(Ex);
                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public JsonResult SaveAsTemplate(MonthlyPlanTemplateInfo templateInfo)
        {
            BaseResult result;

            if (templateInfo != null)
            {
                result = application.SaveMonthlyPlanAsTemplate(templateInfo);
            }
            else
            {
                result = new ValidationResult(ResultStatus.Failure, "TemplateInfo")
                {
                    Message = "Bad Request",
                    Value = HttpStatusCode.BadRequest
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CreateFromTemplate(int? MonthlyPlanTemplateId)
        {
            BaseResult result = new OperationResult();
            if (MonthlyPlanTemplateId != null)
            {
                result = application.CreateFromTemplate(MonthlyPlanTemplateId.Value);
            }
            else
            {
                result = new ValidationResult(ResultStatus.Failure, "MonthlyPlanTemplateId")
                {
                    Message = "Bad Request",
                    Value = HttpStatusCode.BadRequest
                };
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateFromTemplateByTemplateInfoPlanId(MonthlyPlanTemplateInfo templateInfo)
        {
            return CreateFromTemplate(templateInfo.PlanId);
        }

        protected override IList<MonthlyPlanViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            throw new NotImplementedException();
        }

        protected override MonthlyPlanViewModel MapToViewModel(MonthlyBudget model)
        {
            return model.MapToViewModel();
        }

        protected override MonthlyBudget MapToModel(MonthlyPlanViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }

        protected override BaseResult UpdateModel(MonthlyBudget model)
        {
            throw new NotImplementedException();
        }

        protected override BaseResult CreateModel(MonthlyBudget model)
        {
            throw new NotImplementedException();
        }
    }
}