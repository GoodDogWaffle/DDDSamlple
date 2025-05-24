using System;
using System.Threading.Tasks;
namespace Manufacturing.Domain.Repositories
{
    public interface IPricingRepository
    {
        Task<decimal> GetDiscountAsync(Guid itemId,int quantity);
    }
}