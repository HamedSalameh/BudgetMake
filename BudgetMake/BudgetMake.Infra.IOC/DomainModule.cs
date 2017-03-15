using BudgetMake.Domain.BusinessLogic;
using BudgetMake.Shared.Contracts.Domain;
using Ninject.Modules;

namespace BudgetMake.Infra.IOC
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAnnualBudgetBL>().To<AnnualBudgetBL>();
            Bind<IMonthlyBudgetBL>().To<MonthlyBudgetBL>();

            Bind<ISalaryBL>().To<SalaryBL>();
            Bind<IIncomeBL>().To<IncomeBL>();

            Bind<IExpenseBL>().To<ExpenseBL>();
            Bind<IChequeBL>().To<ChequeBL>();
            Bind<ICreditCardBL>().To<CreditCardBL>();

            Bind<IMonthlyPlanTemplateBL>().To<MonthlyPlanTemplateBL>();
        }
    }
}
