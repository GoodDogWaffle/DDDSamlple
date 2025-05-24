// Domain Layer: 注文永続化の抽象
using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Sales;

namespace Manufacturing.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid orderId);
        Task AddAsync(Order order);
    }
}