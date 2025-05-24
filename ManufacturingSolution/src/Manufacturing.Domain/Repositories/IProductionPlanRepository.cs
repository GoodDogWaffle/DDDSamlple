using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Planning;

namespace Manufacturing.Domain.Repositories
{
    public interface IProductionPlanRepository
    {
        Task<ProductionPlan?> GetByIdAsync(Guid id);
        Task AddAsync(ProductionPlan plan);
        Task SaveChangesAsync();
    }
}