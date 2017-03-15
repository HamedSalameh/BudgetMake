using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

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
                handleException(Ex);
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
                handleException(Ex);
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
                            handleException(Ex);
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
        public ActionResult Edit(int AnnualPlandId = 0)
        {
            if (AnnualPlandId != 0)
            {
                AnnualPlanViewModel annualPlanVM = null;
                try
                {
                    annualPlanVM = application.GetAnnualBudget(AnnualPlandId).MapToViewModel();
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
                    handleException(Ex);
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
                            handleException(Ex);
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
                    handleException(Ex);
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
                            handleException(Ex);
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

        public override IList<AnnualPlanViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            throw new NotImplementedException();
        }

        public override AnnualPlanViewModel GetViewModel(AnnualBudget model)
        {
            throw new NotImplementedException();
        }
    }
}