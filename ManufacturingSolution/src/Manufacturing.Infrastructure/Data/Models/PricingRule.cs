using System;

namespace Manufacturing.Infrastructure.Data.Models
{
    /// <summary>
    /// 値引・契約価格ルール
    /// </summary>
    public class PricingRule
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public decimal Discount { get; set; }
    }
}
