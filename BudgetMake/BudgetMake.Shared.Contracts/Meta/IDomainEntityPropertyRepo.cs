using BudgetMake.Shared.Contracts.Infra;
using BudgetMake.Shared.DomainModel.MetaModels;

namespace BudgetMake.Shared.Contracts.Meta
{
    public interface IDomainEntityPropertyRepo : IGenericRepository<DomainEntityPropertyLookupTable>
    {
    }
}
