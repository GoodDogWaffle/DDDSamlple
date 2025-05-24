using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Stocktaking;

namespace Manufacturing.Domain.Repositories
{
    public interface IStocktakeRepository
    {
        Task<StocktakeSession?> GetByIdAsync(Guid id);
        Task AddAsync(StocktakeSession session);
        Task SaveChangesAsync();
    }
}