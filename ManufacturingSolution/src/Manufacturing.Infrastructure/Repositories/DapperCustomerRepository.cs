using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Infrastructure.Repositories
{
    /// <summary>
    /// Dapper による顧客リポジトリ実装
    /// </summary>
    public class DapperCustomerRepository : ICustomerRepository
    {
        readonly string _conn;
        public DapperCustomerRepository(IConfiguration cfg) => _conn = cfg.GetConnectionString("Default")!;

        public async Task<decimal> GetCreditLimitAsync(Guid customerId)
        {
            const string sql = "SELECT CreditLimit FROM Customers WHERE Id = @Id";
            await using var con = new SqlConnection(_conn);
            return await con.QuerySingleOrDefaultAsync<decimal>(sql, new { Id = customerId });
        }
    }
}