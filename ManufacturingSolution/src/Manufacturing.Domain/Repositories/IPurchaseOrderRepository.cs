using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Procurement;

namespace Manufacturing.Domain.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder?> GetByIdAsync(Guid id);
        Task AddAsync(PurchaseOrder order);
        Task SaveChangesAsync();
    }
}