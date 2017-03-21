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

        protected abstract IList<ViewModel> GetViewModelsList(int MonthlyPlanId = 0);
        protected abstract ViewModel GetViewModel(Model model);
        protected abstract Model GetModel(ViewModel ViewModel);
        protected abstract BaseResult UpdateModel(Model model);

        public BaseController(IApplication ApplicaionLayer, ILocalLogger Log)
        {
            // Setup
            application = ApplicaionLayer;
            _log = Log;
            // Init logger
            _log = Log.SetType(typeof(Model));
        }

        protected void HandleException(Exception Ex)
        {
            ExceptionHelpers.HandleException(Ex, _log);
        }
    }

}