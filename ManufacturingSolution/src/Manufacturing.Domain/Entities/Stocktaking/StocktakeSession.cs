using System;
using System.Collections.Generic;
using System.Linq;

namespace Manufacturing.Domain.Entities.Stocktaking
{
    public class StocktakeLine
    {
        public Guid ItemId { get; private set; }
        public int CountedQuantity { get; private set; }
        public int SystemQuantity { get; private set; }
        public int Variance => CountedQuantity - SystemQuantity;

        public StocktakeLine(Guid itemId, int counted, int system)
        {
            ItemId = itemId;
            CountedQuantity = counted;
            SystemQuantity = system;
        }
    }

    public class StocktakeSession
    {
        public Guid Id { get; private set; }
        public DateTime SessionDate { get; private set; }
        private readonly List<StocktakeLine> _lines = new();
        public IEnumerable<StocktakeLine> Lines => _lines;

        private StocktakeSession() { }

        public StocktakeSession(Guid id, DateTime date)
        {
            Id = id;
            SessionDate = date;
        }

        public void AddLine(Guid itemId, int counted, int system)
        {
            _lines.Add(new StocktakeLine(itemId, counted, system));
        }

        public IEnumerable<StocktakeLine> GetVarianceLines() => _lines.Where(l => l.Variance != 0);
    }
}
