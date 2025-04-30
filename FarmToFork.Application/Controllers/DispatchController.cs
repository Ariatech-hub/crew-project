using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmToFork.Application.Controllers
{
    [Route("api/dispatch")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly IDispatchService _dispatchService;

        public DispatchController(IDispatchService dispatchService)
        {
            _dispatchService = dispatchService;
        }

        [HttpGet , Route("available")]
        public async Task<IActionResult> GetAvailableDispatch(int? grainCycleId, int? customerId)
        {
            try
            {
                IEnumerable<DispatchAvailableDto> availableDtos = await _dispatchService.GetavailableDispatchIdTask(grainCycleId ?? 0, customerId ?? 0);
                return Ok(availableDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("process")]
        public async Task<IActionResult> ProcessDispatch(DispatchDto dispatchDto)
        {
            try
            {
                await _dispatchService.ProcessDispatch(dispatchDto);
                return Ok("Dispatched");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<DispatchOverviewDto> dispatchOverviewDtos = await _dispatchService.GetAllDispatch();
                return Ok(dispatchOverviewDtos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
