using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Procurement;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    public class ProcurementService
    {
        private readonly IPurchaseOrderRepository _repo;
        public ProcurementService(IPurchaseOrderRepository repo) => _repo = repo;

        public async Task<Guid> CreatePurchaseAsync(DateTime date, DateTime expected)
        {
            var po = new PurchaseOrder(Guid.NewGuid(), date, expected);
            await _repo.AddAsync(po);
            await _repo.SaveChangesAsync();
            return po.Id;
        }

        public async Task ReceiveGoodsAsync(Guid poId)
        {
            var po = await _repo.GetByIdAsync(poId) ?? throw new InvalidOperationException("Not found");
            po.MarkReceived();
            await _repo.SaveChangesAsync();
        }
    }
}
