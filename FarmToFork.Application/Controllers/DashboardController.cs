using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmToFork.Application.Controllers
{
    [Route("api/dashboard")]
    [ApiController, Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IFarmerService _farmerService;
        private readonly ICustomerService _customerService;
        private readonly IGrainService _grainService;
        
        public DashboardController(
            ILogger<DashboardController> logger, 
            IFarmerService farmerService, 
            ICustomerService customerService,
            IGrainService grainService)
        {
            _logger = logger;
            _farmerService = farmerService;
            _customerService = customerService;
            _grainService = grainService;
        }

        [HttpGet, Route("dashboard"), CheckAccessForMenu(MenuCode = 33)]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
                IEnumerable<FarmerTableDto> farmers = await _farmerService.GetAllFarmerList();
                IEnumerable<CustomerDto> customers = await _customerService.GetAllCustomers();
                IEnumerable<GrainTableDto> grains = await _grainService.GetAllGrains();
                IEnumerable<GrainCycleTableDto> grainCycles = await _grainService.GetAllGrainCycles();
               
                DashboardDto dashboard = new DashboardDto();
                dashboard.FarmerCount = farmers.Count();
                dashboard.CustomerCount = customers.Count();
                dashboard.GrainCount = grains.Count();
                dashboard.GrainCycleCount = grainCycles.Count();
                return Ok(dashboard);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occurred while fetching dashboard data", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
