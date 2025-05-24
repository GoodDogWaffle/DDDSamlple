using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Sales;
using Manufacturing.Domain.Repositories;
using Manufacturing.Domain.Entities.Inventory;

namespace Manufacturing.Application.Services
{
    /// <summary>
    /// Application Layer: 注文ユースケースのオーケストレーションとトランザクション管理
    /// </summary>
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IInventoryRepository _inventoryRepo;

        public OrderService(
            IOrderRepository orderRepo,
            ICustomerRepository customerRepo,
            IInventoryRepository inventoryRepo)
        {
            _orderRepo = orderRepo;
            _customerRepo = customerRepo;
            _inventoryRepo = inventoryRepo;
        }

        /// <summary>
        /// 受注作成 → 与信チェック → 在庫引当 → 保存
        /// </summary>
        public async Task<Guid> CreateAsync(Guid customerId, DateTime orderDate,
                                            IEnumerable<(Guid ItemId, int Quantity)> lines)
        {
            // 1. 与信限度額取得
            var creditLimit = await _customerRepo.GetCreditLimitAsync(customerId);

            // 2. 注文行をドメインオブジェクトに変換
            var orderLines = new List<(Guid, int, decimal)>();
            decimal total = 0m;
            foreach (var (itemId, qty) in lines)
            {
                decimal unitPrice = 100m; // 固定例
                orderLines.Add((itemId, qty, unitPrice));
                total += unitPrice * qty;
            }

            // 3. ドメインモデル生成（与信チェック含む）
            var order = new Order(Guid.NewGuid(), customerId, orderDate, orderLines, creditLimit);
            await _orderRepo.AddAsync(order);

            // 4. 在庫引当
            foreach (var (itemId, qty, _) in orderLines)
            {
                var item = await _inventoryRepo.GetByIdAsync(itemId)
                           ?? throw new InvalidOperationException("Inventory not found");
                item.Reserve(qty);
                await _inventoryRepo.UpdateAsync(item);
            }

            return order.Id;
        }
    }
}
