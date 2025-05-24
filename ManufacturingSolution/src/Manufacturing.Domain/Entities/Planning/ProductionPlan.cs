using System;
using System.Collections.Generic;

namespace Manufacturing.Domain.Entities.Planning
{
    public class ProductionPlan
    {
        public Guid Id { get; private set; }
        public DateTime PlannedDate { get; private set; }
        public IDictionary<Guid, int> RequiredQuantities { get; private set; }
        public int PlantCapacity { get; private set; }

        private ProductionPlan() { }

        public ProductionPlan(Guid id, DateTime plannedDate, IDictionary<Guid,int> req, int capacity)
        {
            if(req == null) throw new ArgumentNullException(nameof(req));
            if(capacity <= 0) throw new ArgumentException("Capacity must be positive", nameof(capacity));

            Id = id;
            PlannedDate = plannedDate;
            RequiredQuantities = new Dictionary<Guid,int>(req);
            PlantCapacity = capacity;
            ValidateCapacity();
        }

        public void UpdatePlan(DateTime newDate, IDictionary<Guid,int> newReq)
        {
            if(newReq == null) throw new ArgumentNullException(nameof(newReq));
            PlannedDate = newDate;
            RequiredQuantities = new Dictionary<Guid,int>(newReq);
            ValidateCapacity();
        }

        private void ValidateCapacity()
        {
            var total = 0;
            foreach(var qty in RequiredQuantities.Values) total += qty;
            if(total > PlantCapacity)
                throw new InvalidOperationException($"Total quantity {total} exceeds capacity {PlantCapacity}");
        }
    }
}
