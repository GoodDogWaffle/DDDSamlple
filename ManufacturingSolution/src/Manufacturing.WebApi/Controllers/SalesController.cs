using Microsoft.AspNetCore.Mvc;
using Manufacturing.Application.Services;
using Manufacturing.Application.DTOs;

namespace Manufacturing.WebApi.Controllers
{
    [ApiController]
    [Route("api/sales")]
    public class SalesController : ControllerBase
    {
        private readonly OrderService _srv;
        public SalesController(OrderService srv) => _srv = srv;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            var id = await _srv.CreateAsync(dto.CustomerId, dto.OrderDate, dto.Lines);
            return CreatedAtAction(nameof(Get), new { orderId = id }, null);
        }

        [HttpGet("{orderId:guid}")]
        public IActionResult Get(Guid orderId) => Ok();
    }
}
