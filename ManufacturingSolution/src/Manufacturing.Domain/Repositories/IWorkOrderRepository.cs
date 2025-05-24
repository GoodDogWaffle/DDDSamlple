using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.ShopFloor;

namespace Manufacturing.Domain.Repositories
{
    public interface IWorkOrderRepository
    {
        Task<WorkOrder?> GetByIdAsync(Guid id);
        Task AddAsync(WorkOrder order);
        Task SaveChangesAsync();
    }
}