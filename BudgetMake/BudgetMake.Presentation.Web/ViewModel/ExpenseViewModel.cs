using System.ComponentModel.DataAnnotations;

namespace BudgetMake.Presentation.Web.ViewModel
{
    public class ExpenseViewModel : BudgetItemViewModelBase
    {
        [Display(Name ="ID", ResourceType = typeof(Shared.Common.Resources.General))]
        public int BudgetItemId { get; set; }
    }
}