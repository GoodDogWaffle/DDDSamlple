using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manufacturing.Domain.Entities.Planning;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    public class ProductionPlanningService
    {
        private readonly IProductionPlanRepository _repo;
        public ProductionPlanningService(IProductionPlanRepository repo) => _repo = repo;

        public async Task<Guid> CreatePlanAsync(DateTime date, IDictionary<Guid,int> req, int capacity)
        {
            var plan = new ProductionPlan(Guid.NewGuid(), date, req, capacity);
            await _repo.AddAsync(plan);
            await _repo.SaveChangesAsync();
            return plan.Id;
        }
    }
}
