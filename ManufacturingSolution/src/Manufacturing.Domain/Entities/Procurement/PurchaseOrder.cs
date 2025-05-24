using System;

namespace Manufacturing.Domain.Entities.Procurement
{
    public class PurchaseOrder
    {
        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime ExpectedArrival { get; private set; }
        public bool Received { get; private set; }

        private PurchaseOrder() { }

        public PurchaseOrder(Guid id, DateTime date, DateTime expectedArrival)
        {
            if(expectedArrival < date) throw new ArgumentException("Expected arrival must be after creation date");
            Id = id;
            CreatedDate = date;
            ExpectedArrival = expectedArrival;
            Received = false;
        }

        public void MarkReceived()
        {
            if(Received) throw new InvalidOperationException("Already received");
            Received = true;
        }
    }
}
