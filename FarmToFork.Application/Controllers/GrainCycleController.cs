namespace FarmToFork.Application.Controllers;

[Route("api")]
[ApiController]
public class GrainCycleController : ControllerBase
{
    private readonly IGrainService _grainService;
    private readonly ILogger<GrainCycleController> _logger;
    public GrainCycleController(IGrainService grainService, ILogger<GrainCycleController> logger)
    {
        _grainService = grainService;
        _logger = logger;
    }
    [HttpGet, Route("grain-cycles"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> GetGrainCycle()
    {
        try
        {
            var grainCycle = await _grainService.GetAllGrainCycles();
            return Ok(grainCycle);
        }
        catch (Exception e)
        {
            _logger.LogError("exception Occurred while getting all grain cycle", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("grain-cycle/{id}"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> GetGrainCycleById(int id)
    {
        try
        {
            var grainCycle = await _grainService.GetGrainCycleById(id);
            return Ok(grainCycle);
        }
        catch (Exception e)
        {
            _logger.LogError("exception Occurred while getting all grain cycle", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("grain-cycle/insert"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> InsertGrainCycle(GrainCycleInsertDto grainCycleInsertDto)
    {
        try
        {
            await _grainService.InsertGrainCycle(grainCycleInsertDto);
            return Ok($"Grain Cycle {grainCycleInsertDto.Name} has been added.");

        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while Inserting Grain Cycle", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("grain-cycle/update"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> UpdateGrainCycle(GrainCycleUpdateDto grainCycleUpdateDto)
    {
        try
        {
            await _grainService.GrainUpdateCycle(grainCycleUpdateDto);
            return Ok($"Grain Cycle {grainCycleUpdateDto.Name} has been updated.");
        }
        catch(Exception e)
        {
            _logger.LogError("Exception Occured while Updating Grain Cycle", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("grain-cycle/delete"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> DeleteGrainCycle(GrainCycleDeleteDto grainCycleDeleteDto)
    {
        try
        {
            var deletedGrainCycle = await _grainService.DeleteGrainCycle(grainCycleDeleteDto);
            return Ok($"Grain Cycle {deletedGrainCycle.Name} has been Deleted");

        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occured while Deleting Grain Cycle", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("grain/{id}/grain-cycles"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> GetGrainCyclesByGrainId(int id)
    {
        try
        {
            IEnumerable<GrainCycleTableDto> grainCycles = await _grainService.GetGrainCyclesByGrainId(id);
            return Ok(grainCycles);
        }
        catch(Exception e)
        {
            _logger.LogError($"Exception occurred while getting grain cycles by grain id, {e.Message}");
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("grain-cycle/update-status"), CheckAccessForMenu(MenuCode = 24)]
    public async Task<IActionResult> UpdateStatus(GrainCycleUpdateStatusDto grainCycle)
    {
        try
        {
            GrainCycleUpdateStatusDto updatedGrainCycle = await _grainService.GrainCycleActiveStatusChanged(grainCycle);
            return Ok($"The Active Status of grain cycle {updatedGrainCycle.Name} has been updated");

        }
        catch(Exception e)
        {
            _logger.LogError($"Exception occurred while updating the active status of grain cycle , {e.Message}");
            return BadRequest(e.Message);
        }
    }
}

