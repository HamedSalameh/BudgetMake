using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using GeneralServices;
using static GeneralServices.Enums;
using GeneralServices.Helpers;
using Newtonsoft.Json;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class AnnualBudgetController : BaseController<AnnualBudget, AnnualPlanViewModel>
    {
        public AnnualBudgetController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            IList<AnnualBudget> annualBudgets = null;

            try
            {
                annualBudgets = application.GetEntities<AnnualBudget>(a => a.MonthlyBudgets); 
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
            }

            return View(annualBudgets);
        }

        [HttpGet]
        public ActionResult GetAnnualPlans()
        {
            IList<AnnualPlanViewModel> annualPlanCardViewModel = null;

            try
            {
                IList<AnnualBudget> annualBudgets = application.GetEntities<AnnualBudget>();
                annualPlanCardViewModel = annualBudgets.Map();
            }
            catch (Exception Ex)
            {
                HandleException(Ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            return View("AnnualPlans", annualPlanCardViewModel);
        }

        [HttpGet]
        public ActionResult CreatePlan()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePlan(AnnualPlanViewModel annualPlanVM)
        {
            if (annualPlanVM != null)
            {
                if (ModelState.IsValid)
                {
                    AnnualBudget annualBudget = annualPlanVM.MapToModel();
                    if (annualBudget != null)
                    {
                        try
                        {
                            application.DefaultAddEntity(annualBudget);
                            return RedirectToAction("GetAnnualPlans");
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
                    return View(annualPlanVM);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public PartialViewResult Edit(int AnnualPlandId = 0)
        {
            List<BaseResult> results = new List<BaseResult>();
            AnnualPlanViewModel viewModel = null;

            if (AnnualPlandId != 0)
            {
                try
                {
                    AnnualBudget annual = application.GetAnnualBudget(AnnualPlandId);
                    if (annual != null)
                    {
                        viewModel = MapToViewModel(annual);
                        ViewName = "EditAnnualPlan";
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
        public ActionResult Edit(AnnualPlanViewModel annualPlanVM)
        {
            if (annualPlanVM != null)
            {
                if (ModelState.IsValid)
                {
                    AnnualBudget annualBudget = annualPlanVM.MapToModel();
                    if (annualBudget != null)
                    {
                        try
                        {
                            application.DefaultUpdateEntity(annualBudget);
                            return RedirectToAction("GetAnnualPlans");  // 200 OK
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
                    return View(annualPlanVM);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult Delete(int AnnualPlanId = 0)
        {
            if (AnnualPlanId != 0)
            {
                AnnualPlanViewModel annualPlanVM = null;
                try
                {
                    annualPlanVM = application.GetAnnualBudget(AnnualPlanId).MapToViewModel();
                    if (annualPlanVM != null)
                    {
                        return View(annualPlanVM);
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
        public ActionResult Delete(int? AnnualPlanId)
        {
            if (AnnualPlanId != null)
            {
                if (ModelState.IsValid)
                {
                    AnnualBudget annualBudget = application.GetAnnualBudget(AnnualPlanId.Value);
                    if (annualBudget != null)
                    {
                        try
                        {
                            application.DeleteAnnualBudget(annualBudget);
                            return RedirectToAction("GetAnnualPlans");
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

        protected override IList<AnnualPlanViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            throw new NotImplementedException();
        }

        protected override AnnualPlanViewModel MapToViewModel(AnnualBudget model)
        {
            return model.MapToViewModel();
        }

        protected override AnnualBudget MapToModel(AnnualPlanViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }

        protected override BaseResult UpdateModel(AnnualBudget model)
        {
            throw new NotImplementedException();
        }

        protected override BaseResult CreateModel(AnnualBudget model)
        {
            throw new NotImplementedException();
        }
    }
}