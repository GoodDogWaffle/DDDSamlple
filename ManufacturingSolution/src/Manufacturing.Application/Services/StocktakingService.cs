using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Stocktaking;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    public class StocktakingService
    {
        private readonly IStocktakeRepository _repo;
        public StocktakingService(IStocktakeRepository repo) => _repo = repo;

        public async Task<Guid> CreateSessionAsync(DateTime date)
        {
            var session = new StocktakeSession(Guid.NewGuid(), date);
            await _repo.AddAsync(session);
            await _repo.SaveChangesAsync();
            return session.Id;
        }

        public async Task RecordCountAsync(Guid sessionId, IEnumerable<(Guid itemId,int counted,int system)> lines)
        {
            var session = await _repo.GetByIdAsync(sessionId) ?? throw new InvalidOperationException("Not found");
            foreach(var (id, cnt, sys) in lines)
                session.AddLine(id, cnt, sys);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<StocktakeLine>> GetVariancesAsync(Guid sessionId)
        {
            var session = await _repo.GetByIdAsync(sessionId) ?? throw new InvalidOperationException("Not found");
            return session.GetVarianceLines();
        }
    }
}
