using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class ChequeViewModelExtentions
    {
        public static ChequeViewModel MapToViewModel(this Cheque Cheque)
        {
            ChequeViewModel viewModel = null;

            if (Cheque != null)
            {
                viewModel = new ChequeViewModel
                {
                    Amount = Cheque.Amount,
                    ChequeId = Cheque.Id,
                    Comments = Cheque.Comments,
                    Description = Cheque.Description,
                    MonthlyBudgetId = Cheque.MonthlyBudgetId,
                    Payee = Cheque.Payee,
                    PaymentDate = Cheque.PaymentDate,
                    CreationDate = Cheque.CreationDate,
                    LastModified = Cheque.LastModifited
                };
            }

            return viewModel;
        }

        public static Cheque MapToModel(this ChequeViewModel viewModel)
        {
            Cheque cheque = null;

            if (viewModel != null)
            {
                cheque = new Cheque
                {
                    Amount = viewModel.Amount,
                    Comments = viewModel.Comments,
                    Description = viewModel.Description,
                    Id = viewModel.ChequeId,
                    MonthlyBudgetId = viewModel.MonthlyBudgetId,
                    Payee = viewModel.Payee,
                    PaymentDate = viewModel.PaymentDate,
                    CreationDate = viewModel.CreationDate,
                    LastModifited = viewModel.LastModified
                };
            }

            return cheque;
        }

        public static IList<ChequeViewModel> MapToViewModelsList(this IList<Cheque> cheques)
        {
            List<ChequeViewModel> viewModels = new List<ChequeViewModel>();

            if (cheques != null && cheques.Count > 0)
            {
                foreach (Cheque c in cheques)
                {
                    ChequeViewModel viewModel = c.MapToViewModel();
                    if (viewModel != null)
                    {
                        viewModels.Add(viewModel);
                    }
                }
            }

            return viewModels;
        }
    }
}