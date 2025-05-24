using System;

namespace Manufacturing.Domain.Entities.ShopFloor
{
    public enum WorkOrderStatus { Planned, InProgress, Completed }

    public class WorkOrder
    {
        public Guid Id { get; private set; }
        public DateTime ScheduledStart { get; private set; }
        public WorkOrderStatus Status { get; private set; }

        private WorkOrder() { }

        public WorkOrder(Guid id, DateTime start)
        {
            Id = id;
            ScheduledStart = start;
            Status = WorkOrderStatus.Planned;
        }

        public void Start()
        {
            if(Status != WorkOrderStatus.Planned)
                throw new InvalidOperationException("Work order can only start from Planned status");
            Status = WorkOrderStatus.InProgress;
        }

        public void Complete()
        {
            if(Status != WorkOrderStatus.InProgress)
                throw new InvalidOperationException("Work order must be InProgress to complete");
            Status = WorkOrderStatus.Completed;
        }
    }
}
