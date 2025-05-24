using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Inventory;

namespace Manufacturing.Domain.Repositories
{
    /// <summary>
    /// 在庫アイテム取得・更新リポジトリの抽象
    /// </summary>
    public interface IInventoryRepository
    {
        Task<InventoryItem?> GetByIdAsync(Guid itemId);
        Task UpdateAsync(InventoryItem item);
    }
}
