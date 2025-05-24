using System;
using System.Collections.Generic;

namespace Manufacturing.Application.DTOs
{
    /// <summary>
    /// WebApi 層からの注文作成 DTO
    /// </summary>
    public class CreateOrderDto
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<(Guid ItemId, int Quantity)> Lines { get; set; } = new();
    }
}
