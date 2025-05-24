using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.ShopFloor;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    public class ShopFloorService
    {
        private readonly IWorkOrderRepository _repo;
        public ShopFloorService(IWorkOrderRepository repo) => _repo = repo;

        public async Task<Guid> ScheduleWorkAsync(DateTime start)
        {
            var wo = new WorkOrder(Guid.NewGuid(), start);
            await _repo.AddAsync(wo);
            await _repo.SaveChangesAsync();
            return wo.Id;
        }

        public async Task CompleteWorkAsync(Guid workOrderId)
        {
            var wo = await _repo.GetByIdAsync(workOrderId) ?? throw new InvalidOperationException("Not found");
            wo.Start();
            wo.Complete();
            await _repo.SaveChangesAsync();
        }
    }
}
