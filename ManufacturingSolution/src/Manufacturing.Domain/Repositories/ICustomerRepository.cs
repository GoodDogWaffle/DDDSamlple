using System;
using System.Threading.Tasks;
namespace Manufacturing.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task<decimal> GetCreditLimitAsync(Guid customerId);
    }
}