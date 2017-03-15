using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class CreditCardBL : BaseBL<CreditCard>, ICreditCardBL
    {
        public CreditCardBL(ICreditCardRepo CreditCardRepo, ILocalLogger Log) : base(CreditCardRepo, Log)
        {

        }
    }
}
