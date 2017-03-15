using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class LoadPaymentExtentions
    {
        public static LoanPaymentViewModel MapToViewModel(this LoanPayment LoanPayment)
        {
            LoanPaymentViewModel viewModel = null;

            if (LoanPayment != null)
            {
                viewModel = new LoanPaymentViewModel
                {
                    Amount = LoanPayment.Amount,
                    LoanPaymentId = LoanPayment.Id,
                    Comments = LoanPayment.Comments,
                    Description = LoanPayment.Description,
                    MonthlyBudgetId = LoanPayment.MonthlyBudgetId,
                    PaymentDate = LoanPayment.PaymentDate,
                    LoanId = LoanPayment.LoanId
                };
            }

            return viewModel;
        }

        public static LoanPayment MapToModel(this LoanPaymentViewModel viewModel)
        {
            LoanPayment loanPayment = null;

            if (viewModel != null)
            {
                loanPayment = new LoanPayment
                {
                    Amount = viewModel.Amount,
                    Comments = viewModel.Comments,
                    Description = viewModel.Description,
                    Id = viewModel.LoanPaymentId,
                    MonthlyBudgetId = viewModel.MonthlyBudgetId,
                    PaymentDate = viewModel.PaymentDate,
                    LoanId = viewModel.LoanId
                };
            }

            return loanPayment;
        }

        public static IList<LoanPaymentViewModel> MapToViewModelsList(this IList<LoanPayment> loanPayments)
        {
            List<LoanPaymentViewModel> viewModels = new List<LoanPaymentViewModel>();

            if (loanPayments != null && loanPayments.Count > 0)
            {
                foreach (LoanPayment c in loanPayments)
                {
                    LoanPaymentViewModel viewModel = c.MapToViewModel();
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