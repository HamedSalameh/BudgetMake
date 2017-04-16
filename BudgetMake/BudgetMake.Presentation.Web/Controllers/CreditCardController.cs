using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class CreditCardController : BudgetItemBaseController<CreditCard, CreditCardViewModel>
    {
        public CreditCardController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {
            PartialViewNameFor_ItemsList = "Expenses";
            PartialViewNameFor_CreateItem = "CreateExpenseItem";
            PartialViewNameFor_EditItem = "EditExpenseItem";
            PartialViewNameFor_DeleteItem = "DeleteExpenseItem";
        }

        protected override IList<CreditCardViewModel> GetViewModelsList(int MonthlyPlanId)
        {
            return application.GetEntities<CreditCard>(e => e.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
        }

        protected override CreditCardViewModel MapToViewModel(CreditCard model)
        {
            return model.MapToViewModel();
        }

        protected override CreditCard MapToModel(CreditCardViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }
    }
}