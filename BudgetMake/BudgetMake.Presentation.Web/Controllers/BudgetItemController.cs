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
    public class BudgetItemController : BaseController<Expense, ExpenseViewModel>
    {
        public BudgetItemController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {

        }

        [HttpGet]
        public PartialViewResult GetCheques(int MonthlyPlanId = 0)
        {
            IList<ChequeViewModel> Cheques = new List<ChequeViewModel>();
            if (MonthlyPlanId != 0)
            {
                try
                {
                    Cheques = application.GetEntities<Cheque>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                }
            }
            return PartialView("_ChequesDetails", Cheques); ;
        }

        [HttpGet]
        public PartialViewResult GetCreditCards(int MonthlyPlanId = 0)
        {
            IList<CreditCardViewModel> CreditCards = new List<CreditCardViewModel>();
            if (MonthlyPlanId != 0)
            {
                try
                {
                    CreditCards = application.GetEntities<CreditCard>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                }
            }
            return PartialView("_CreditCardDetails", CreditCards);
        }

        [HttpGet]
        public PartialViewResult GetLoanPayments(int MonthlyPlanId = 0)
        {
            IList<LoanPaymentViewModel> loanPayments = new List<LoanPaymentViewModel>();
            if (MonthlyPlanId != 0)
            {
                try
                {
                    loanPayments = application.GetEntities<LoanPayment>(s => s.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                }
            }
            return PartialView("_LoanPaymentsDetails", loanPayments);
        }

        [HttpGet]
        public ActionResult GetBudgetItems(int MonthlyPlanId = 0)
        {
            if (MonthlyPlanId == 0)
            {
                // in case we get here with no monthly plan id, redirect back to monthly plans
                return RedirectToAction("GetMonthlyPlans", "MonthlyPlans", MonthlyPlanId);
            }
            else
            {
                try
                {
                    TempData["MonthlyPlan"] = MonthlyPlanId;
                    TempData["AnnualPlan"] = application.GetMonthlyBudget(MonthlyPlanId).AnnualBudgetId;
                    return View("BudgetItems");
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
        }

        [HttpGet]
        public ActionResult Create(int? MonthlyPlanId)
        {
            ExpenseViewModel viewModel = new ExpenseViewModel();
            viewModel.MonthlyBudgetId = MonthlyPlanId.Value;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExpenseViewModel expenseViewModel)
        {
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
                            //result = application.CreateBudget(budgetItem);
                            result = application.CreateBudgetItem(budgetItem);
                            if (result.Status != ResultStatus.Success)
                            {
                                ViewBag[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(result);
                                return View(expenseViewModel);
                            }
                            return RedirectToAction("GetBudgetItems", new { MonthlyPlanId = budgetItem.MonthlyBudgetId });
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
                    return View(expenseViewModel);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult Edit(int? budgetItemId)
        {
            if (budgetItemId != null)
            {
                ExpenseViewModel viewModel = null;
                try
                {
                    viewModel = application.GetById<Expense>(budgetItemId.Value).MapToViewModel();
                    if (viewModel != null)
                    {
                        return View(viewModel);
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                catch (Exception Ex)
                {
                    HandleException(Ex);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public ActionResult Delete(int budgetItemId = 0)
        {
            if (budgetItemId != 0)
            {
                ExpenseViewModel viewModel = null;
                try
                {
                    viewModel = application.GetById<Expense>(budgetItemId).MapToViewModel();
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
        public ActionResult Delete(int? budgetItemId)
        {
            BaseResult result = null;
            if (budgetItemId != null)
            {
                if (ModelState.IsValid)
                {
                    Expense budgetItem = application.GetById<Expense>(budgetItemId.Value);
                    if (budgetItem != null)
                    {
                        try
                        {
                            result = application.DeleteBudget(budgetItem);
                            if (result.Status != ResultStatus.Success)
                            {
                                ViewBag[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(result);
                                return View();
                            }
                            return RedirectToAction("GetBudgetItems", new { MonthlyPlanId = budgetItem.MonthlyBudgetId });
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

        protected override IList<ExpenseViewModel> GetViewModelsList(int MonthlyPlanId = 0)
        {
            throw new NotImplementedException();
        }

        protected override ExpenseViewModel GetViewModel(Expense model)
        {
            throw new NotImplementedException();
        }

        protected override Expense GetModel(ExpenseViewModel ViewModel)
        {
            throw new NotImplementedException();
        }

        protected override BaseResult UpdateModel(Expense model)
        {
            throw new NotImplementedException();
        }

        protected override BaseResult CreateModel(Expense model)
        {
            throw new NotImplementedException();
        }
    }
}