using GeneralServices.Interfaces;

namespace BudgetMake.Shared.Contracts.Infra
{
    public interface IGenericRepository<T> : IGenericRepositoryBase<T> where T : class
    {
        
    }
}
