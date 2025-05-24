using System;
using System.Threading.Tasks;
using Manufacturing.Domain.Repositories;

namespace Manufacturing.Application.Services
{
    /// <summary>
    /// 価格計算を行うサービス
    /// </summary>
    public class PricingService
    {
        private readonly IPricingRepository _pricingRepo;

        public PricingService(IPricingRepository pricingRepo) => _pricingRepo = pricingRepo;

        /// <summary>
        /// 割引単価を取得。数量に応じたスライディングスケール割引を適用
        /// </summary>
        public async Task<decimal> GetDiscountedPriceAsync(Guid itemId, int quantity)
        {
            // ベース単価は外部 API やマスタから取得（仮定して固定値）
            decimal basePrice = 100m;
            decimal discount = await _pricingRepo.GetDiscountAsync(itemId, quantity);
            return basePrice * (1 - discount);
        }
    }
}
