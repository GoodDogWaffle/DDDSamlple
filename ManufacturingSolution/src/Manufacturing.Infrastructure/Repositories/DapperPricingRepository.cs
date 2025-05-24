using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Infrastructure.Repositories
{
    /// <summary>
    /// Dapper による価格リポジトリ実装
    /// </summary>
    public class DapperPricingRepository : IPricingRepository
    {
        readonly string _conn;
        public DapperPricingRepository(IConfiguration cfg)=>_conn = cfg.GetConnectionString("Default")!;

        public async Task<decimal> GetDiscountAsync(Guid itemId,int quantity)
        {
            const string sql = @"
SELECT TOP 1 Discount
  FROM PricingRules
 WHERE ItemId = @ItemId
 ORDER BY Discount DESC"; // ここでは最も高い割引を適用
            await using var con = new SqlConnection(_conn);
            var discount = await con.QuerySingleOrDefaultAsync<decimal?>(sql,new{ItemId=itemId});
            return discount ?? 0m;
        }
    }
}