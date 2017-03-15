using BudgetMake.Shared.Contracts.Domain;
using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel;

namespace BudgetMake.Domain.BusinessLogic
{
    public class SalaryBL : BaseBL<Salary>, ISalaryBL
    {
        public SalaryBL(ISalaryRepo SalaryRepo, ILocalLogger Log) : base(SalaryRepo, Log)
        {

        }
    }
}
