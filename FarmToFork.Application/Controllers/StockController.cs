using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmToFork.Application.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllStock(int? grainCycleId)
        {
            try
            {
                IEnumerable<StockDto> stockDtos = await _stockService.GetAllStock(grainCycleId ?? 0);
                return Ok(stockDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
