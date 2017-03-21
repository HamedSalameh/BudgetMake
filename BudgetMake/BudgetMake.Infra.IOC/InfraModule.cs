using Ninject.Modules;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Infra.Repository;
using BudgetMake.Infra.Logging;
using System;
using Ninject.Activation;

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

            //Bind<ILocalLogger>().To<LocalLogger>();
        }
    }

    public class LoggingModule : NinjectModule
    {
        public override void Load()
        {
            Bind<log4net.ILog>().ToMethod(x => log4net.LogManager.GetLogger(GetParentTypeName(x)))
                .InSingletonScope();

            Bind<ILocalLogger>().To<LocalLogger>()
                .InSingletonScope();
        }

        private string GetParentTypeName(IContext context)
        {
            //return context.Request.ParentContext.Request.ParentContext.Request.Service.FullName;
            return context.Request.ParentRequest.ParentRequest.Target.Member.DeclaringType.ToString();
        }
    }
}
