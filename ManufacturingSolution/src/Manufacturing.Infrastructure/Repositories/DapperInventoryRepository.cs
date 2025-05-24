using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Manufacturing.Domain.Entities.Inventory;
using Manufacturing.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Manufacturing.Infrastructure.Repositories
{
    /// <summary>
    /// Dapper + SQL Server 2019 で在庫取得・更新を実装
    /// </summary>
    public class DapperInventoryRepository : IInventoryRepository
    {
        private readonly string _connStr;
        public DapperInventoryRepository(IConfiguration config) => _connStr = config.GetConnectionString("Default");

        public async Task<InventoryItem?> GetByIdAsync(Guid itemId)
        {
            const string sql = @"SELECT Id, Quantity, ReorderThreshold FROM InventoryItems WHERE Id = @Id";
            using var conn = new SqlConnection(_connStr);
            var dto = await conn.QuerySingleOrDefaultAsync<InventoryDto>(sql, new { Id = itemId });
            return dto == null ? null : new InventoryItem(dto.Id, dto.Quantity, dto.ReorderThreshold);
        }

        public async Task UpdateAsync(InventoryItem item)
        {
            const string sql = @"UPDATE InventoryItems SET Quantity = @Quantity WHERE Id = @Id";
            using var conn = new SqlConnection(_connStr);
            await conn.ExecuteAsync(sql, new { item.Id, item.Quantity });
        }

        private class InventoryDto { public Guid Id; public int Quantity; public int ReorderThreshold; }
    }
}
