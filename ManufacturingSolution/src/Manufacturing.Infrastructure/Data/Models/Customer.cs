// インフラの DB モデル: 顧客マスタ
using System;

namespace Manufacturing.Infrastructure.Data.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
    }
}