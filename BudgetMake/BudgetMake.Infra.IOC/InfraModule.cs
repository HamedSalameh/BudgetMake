using Ninject.Modules;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Infra.Repository;
using BudgetMake.Infra.Logging;

namespace BudgetMake.Infra.IOC
{
    public class InfraModule : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IAnnualBudgetRepo>().To<AnnualBudgetRepo>();
            Bind<IMonthlyBudgetRepo>().To<MonthlyBudgetRepo>();

            Bind<ISalaryRepo>().To<SalaryRepo>();
            Bind<IIncomeRepo>().To<IncomeRepo>();

            Bind<IExpenseRepo>().To<ExpenseRepo>();
            Bind<IChequeRepo>().To<ChequeRepo>();
            Bind<ICreditCardRepo>().To<CreditCardRepo>();

            Bind<IMonthlyPlanTemplateRepo>().To<MonthlyPlanTemplateRepo>();

            Bind<ILocalLogger>().To<LocalLogger>();
        }
    }
}
