using System;

namespace Manufacturing.Domain.Entities.Inventory
{
    /// <summary>
    /// Domain Layer: 在庫アイテムのビジネスロジック
    /// </summary>
    public class InventoryItem
    {
        public Guid Id { get; private set; }
        public int Quantity { get; private set; }
        public int ReorderThreshold { get; private set; }

        private InventoryItem() { }

        public InventoryItem(Guid id, int qty, int threshold)
        {
            if (qty < 0 || threshold < 0)
                throw new ArgumentException("Quantity and threshold must be non-negative.");
            Id = id; Quantity = qty; ReorderThreshold = threshold;
        }

        public void Reserve(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
            if (Quantity < quantity) throw new InvalidOperationException("Insufficient inventory.");
            Quantity -= quantity;
        }

        public bool NeedsReorder() => Quantity <= ReorderThreshold;
    }
}
