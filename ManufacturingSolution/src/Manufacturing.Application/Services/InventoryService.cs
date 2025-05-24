using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository _inventory;
        public InventoryService(IInventoryRepository inventory) => _inventory = inventory;

        public async Task<bool> AllocateAsync(Guid itemId, int quantity)
        {
            var item = await _inventory.GetByIdAsync(itemId);
            if (item == null || item.Quantity < quantity) return false;
            item.Reserve(quantity);
            await _inventory.SaveChangesAsync();
            return true;
        }
    }
}
