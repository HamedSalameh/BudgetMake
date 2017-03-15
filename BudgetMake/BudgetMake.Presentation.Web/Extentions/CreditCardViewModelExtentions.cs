using BudgetMake.Presentation.Web.ViewModel;
using BudgetMake.Shared.DomainModel;
using System.Collections.Generic;

namespace BudgetMake.Presentation.Web.Extentions
{
    public static class CreditCardViewModelExtentions
    {
        public static CreditCardViewModel MapToViewModel(this CreditCard CreditCard)
        {
            CreditCardViewModel viewModel = null;

            if (CreditCard != null)
            {
                viewModel = new CreditCardViewModel
                {
                    Amount = CreditCard.Amount,
                    CreditCardId = CreditCard.Id,
                    Comments = CreditCard.Comments,
                    Description = CreditCard.Description,
                    MonthlyBudgetId = CreditCard.MonthlyBudgetId,
                    PaymentDate = CreditCard.PaymentDate,
                    CardType = CreditCard.CardType
                };
            }

            return viewModel;
        }

        public static CreditCard MapToModel(this CreditCardViewModel viewModel)
        {
            CreditCard creditCard = null;

            if (viewModel != null)
            {
                creditCard = new CreditCard
                {
                    Amount = viewModel.Amount,
                    Comments = viewModel.Comments,
                    Description = viewModel.Description,
                    Id = viewModel.CreditCardId,
                    MonthlyBudgetId = viewModel.MonthlyBudgetId,
                    PaymentDate = viewModel.PaymentDate,
                    CardType = viewModel.CardType
                };
            }

            return creditCard;
        }

        public static IList<CreditCardViewModel> MapToViewModelsList(this IList<CreditCard> creditCards)
        {
            List<CreditCardViewModel> viewModels = new List<CreditCardViewModel>();

            if (creditCards != null && creditCards.Count > 0)
            {
                foreach (CreditCard c in creditCards)
                {
                    CreditCardViewModel viewModel = c.MapToViewModel();
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