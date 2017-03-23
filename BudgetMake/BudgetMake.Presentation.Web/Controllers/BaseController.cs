using BudgetMake.Presentation.Web.Helpers;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using GeneralServices;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
        protected abstract BaseResult CreateModel(Model model);

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