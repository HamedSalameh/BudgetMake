﻿using BudgetMake.Presentation.Web.Extentions;
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
using BudgetMake.Presentation.Web.Helpers;

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
        public PartialViewResult CreatePlan()
        {
            return PartialView("CreateAnnualPlan");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreatePlan(AnnualPlanViewModel annualPlanVM)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;
            
            if (annualPlanVM != null)
            {
                if (ModelState.IsValid)
                {
                    AnnualBudget annualBudget = annualPlanVM.MapToModel();
                    if (annualBudget != null)
                    {
                        try
                        {
                            result = application.CreateAnnualBudget(annualBudget);
                            if (result.Status != ResultStatus.Success)
                            {
                                ViewBag[Consts.OPERATION_RESULT] = JsonConvert.SerializeObject(result);
                            }
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
        public JsonResult Edit(AnnualPlanViewModel annualPlanVM)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;

            if (annualPlanVM != null)
            {
                if (ModelState.IsValid)
                {
                    AnnualBudget annualBudget = annualPlanVM.MapToModel();
                    if (annualBudget != null)
                    {
                        try
                        {
                            result = UpdateModel(annualBudget);
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
        public PartialViewResult Delete(int AnnualPlanId = 0)
        {
            List<BaseResult> results = new List<BaseResult>();
            AnnualPlanViewModel viewModel = null;

            if (AnnualPlanId != 0)
            {
                AnnualBudget model = null;
                try
                {
                    model = application.GetAnnualBudget(AnnualPlanId);
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
            return PartialView("DeleteAnnualPlan", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int? AnnualPlanId)
        {
            List<BaseResult> results = new List<BaseResult>();
            BaseResult result = null;

            if (AnnualPlanId != null)
            {
                try
                {
                    AnnualBudget annualBudget = application.GetAnnualBudget(AnnualPlanId.Value);
                    if (annualBudget != null)
                    {
                        int annualBudgetPlanId = annualBudget.Id;
                        result = application.DeleteAnnualBudget(annualBudget);
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

            if (result != null)
            {
                results.Add(result);
            }

            return Json(results, JsonRequestBehavior.AllowGet);
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
            return application.UpdateAnnualPlan(model);
        }

        protected override BaseResult CreateModel(AnnualBudget model)
        {
            throw new NotImplementedException();
        }
    }
}