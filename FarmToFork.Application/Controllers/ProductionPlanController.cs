using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmToFork.Application.Controllers
{
    [Route("api/production-plan")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanService _productionPlanService;

        public ProductionPlanController(IProductionPlanService productionPlanService)
        {
            _productionPlanService = productionPlanService;
        }

        [HttpPost , Route("list")]
        public async Task<IActionResult> GetAllProductionPlan(ProductionPlanSearchDto productionPlanSearchDto)
        {
            try
            {
                IEnumerable<ProductionPlanViewDto> productionPlans = await _productionPlanService.GetAll(productionPlanSearchDto);
                return Ok(productionPlans);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
