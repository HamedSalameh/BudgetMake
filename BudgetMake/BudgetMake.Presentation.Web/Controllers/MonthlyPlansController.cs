using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using GeneralServices;
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
        public ActionResult CreatePlan(int? AnnualPlanId)
        {
            if (AnnualPlanId != null && AnnualPlanId.Value != 0)
            {
                MonthlyPlanViewModel model = new MonthlyPlanViewModel();
                model.AnnualBudgetId = AnnualPlanId.Value;
                return View(model);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public ActionResult Edit(int MonthlyPlanId)
        {
            if (MonthlyPlanId != 0)
            {
                MonthlyPlanViewModel viewModel = null;
                try
                {
                    viewModel = application.GetMonthlyBudget(MonthlyPlanId).MapToViewModel();
                    if (viewModel != null)
                    {
                        return View(viewModel);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MonthlyPlanViewModel monthlyPlan)
        {
            if (monthlyPlan != null)
            {
                if (ModelState.IsValid)
                {
                    MonthlyBudget monthlyBudget = monthlyPlan.MapToModel();
                    if (monthlyBudget != null)
                    {
                        try
                        {
                            application.DefaultUpdateEntity(monthlyBudget);
                            return RedirectToAction("GetMonthlyPlans", new { AnnualPlanID = monthlyBudget.AnnualBudgetId });  // 200 OK
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
        public ActionResult Delete(int MonthlyPlanId = 0)
        {
            if (MonthlyPlanId != 0)
            {
                MonthlyPlanViewModel viewModel = null;
                try
                {
                    viewModel = application.GetMonthlyBudget(MonthlyPlanId).MapToViewModel();
                    if (viewModel != null)
                    {
                        return View(viewModel);
                    }
                    else
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                    }
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

        public override IList<MonthlyPlanViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            throw new NotImplementedException();
        }

        public override MonthlyPlanViewModel GetViewModel(MonthlyBudget model)
        {
            throw new NotImplementedException();
        }
    }
}