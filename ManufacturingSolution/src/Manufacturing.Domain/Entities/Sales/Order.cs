// Domain Layer: 注文エンティティ
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manufacturing.Domain.Entities.Sales
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public IEnumerable<(Guid ItemId, int Quantity, decimal UnitPrice)> Lines { get; private set; }
        public decimal TotalAmount => Lines.Sum(x => x.Quantity * x.UnitPrice);
        public decimal CreditLimit { get; private set; }

        private Order() { }

        // 与信チェックを含むコンストラクタ
        public Order(Guid id, Guid customerId, DateTime date,
                     IEnumerable<(Guid, int, decimal)> lines, decimal creditLimit)
        {
            if(!lines.Any()) throw new ArgumentException("注文には少なくとも1行が必要です。");
            if(creditLimit < 0) throw new ArgumentException("与信限度額は0以上でなければなりません。");

            Id = id;
            CustomerId = customerId;
            OrderDate = date;
            Lines = lines;
            CreditLimit = creditLimit;
            ValidateCredit();
        }

        private void ValidateCredit()
        {
            if(TotalAmount > CreditLimit)
                throw new InvalidOperationException($"合計 {TotalAmount:C} が与信限度 {CreditLimit:C} を超過しました。");
        }
    }
}