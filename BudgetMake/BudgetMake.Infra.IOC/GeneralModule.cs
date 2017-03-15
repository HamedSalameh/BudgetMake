using BudgetMake.Domain.Application;
using BudgetMake.Shared.Contracts.Domain;
using Ninject.Modules;

namespace BudgetMake.Infra.IOC
{
    public class GeneralModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplication>().To<Application>();
        }
    }
}
