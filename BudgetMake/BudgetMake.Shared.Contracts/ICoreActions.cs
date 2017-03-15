using BudgetMake.Shared.Contracts.Infra;
using System;
using System.Linq.Expressions;

namespace BudgetMake.Shared.Contracts
{
    public interface ICoreActions<T> : IGenericRepository<T> where T : class
    {
        T GetById(int? Id, params Expression<Func<T, object>>[] navigationProperties);
    }
}
