using BudgetMake.Presentation.Web.Extentions;
using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Controllers
{
    public class ChequeController : BudgetItemBaseController<Cheque, ChequeViewModel>
    {
        public ChequeController(IApplication Application, ILocalLogger Log) : base(Application, Log)
        {
            PartialViewNameFor_ItemsList = "Expenses";
            PartialViewNameFor_CreateItem = "CreateExpenseItem";
            PartialViewNameFor_EditItem = "EditExpenseItem";
            PartialViewNameFor_DeleteItem = "DeleteExpenseItem";
        }

        protected override IList<ChequeViewModel> GetViewModelsList(int MonthlyPlanId)
        {
            return application.GetEntities<Cheque>(e => e.MonthlyBudgetId == MonthlyPlanId).MapToViewModelsList();
        }

        protected override ChequeViewModel MapToViewModel(Cheque model)
        {
            return model.MapToViewModel();
        }

        protected override Cheque MapToModel(ChequeViewModel ViewModel)
        {
            return ViewModel.MapToModel();
        }
    }
}