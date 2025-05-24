using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Manufacturing.Domain.Repositories;
using Manufacturing.Domain.Entities.Sales;

namespace Manufacturing.Infrastructure.Repositories
{
    /// <summary>
    /// 注文を Dapper + 明示的トランザクションで保存
    /// </summary>
    public class DapperOrderRepository : IOrderRepository
    {
        readonly string _conn;
        public DapperOrderRepository(IConfiguration cfg) => _conn = cfg.GetConnectionString("Default")!;

        public async Task AddAsync(Order order)
        {
            const string sqlOrder = @"INSERT INTO Orders (Id,CustomerId,OrderDate,CreditLimit)
VALUES (@Id,@CustomerId,@OrderDate,@CreditLimit);";

            const string sqlLines = @"INSERT INTO OrderLines (OrderId,ItemId,Quantity,UnitPrice)
VALUES (@OrderId,@ItemId,@Quantity,@UnitPrice);";

            await using var con = new SqlConnection(_conn);
            await con.OpenAsync();
            using var tx = con.BeginTransaction();
            await con.ExecuteAsync(sqlOrder, new
            {
                order.Id,
                order.CustomerId,
                order.OrderDate,
                CreditLimit = order.CreditLimit
            }, tx);

            var lineParams = order.Lines.Select(l => new
            {
                OrderId = order.Id,
                ItemId = l.ItemId,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice
            });
            await con.ExecuteAsync(sqlLines, lineParams, tx);
            await tx.CommitAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid orderId)
        {
            const string sqlOrder = @"SELECT Id,CustomerId,OrderDate,CreditLimit FROM Orders WHERE Id=@Id";
            const string sqlLines = @"SELECT ItemId,Quantity,UnitPrice FROM OrderLines WHERE OrderId=@Id";

            await using var con = new SqlConnection(_conn);
            var orderRow = await con.QuerySingleOrDefaultAsync(sqlOrder,new{Id=orderId});
            if(orderRow==null) return null;
            var lines = (await con.QueryAsync(sqlLines,new{Id=orderId}))
                .Select(r => ((Guid)r.ItemId,(int)r.Quantity,(decimal)r.UnitPrice));
            return new Order(orderRow.Id, orderRow.CustomerId, orderRow.OrderDate, lines, orderRow.CreditLimit);
        }
    }
}