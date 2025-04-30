

namespace FarmToFork.Application.Controllers;

[Route("api/general")]
[ApiController, Authorize]
public class GeneralController : ControllerBase
{
    private readonly ILogger<GeneralController> _logger;
    private readonly IGeneralService _generalService;
    private readonly IDistrictService _districtService;
    private readonly IPalikaService _palikaService;
    private readonly IProvinceService _provinceService;


    public GeneralController(ILogger<GeneralController> logger, IGeneralService generalService, IDistrictService districtService, IPalikaService palikaService, IProvinceService provinceService)
    {
        _logger = logger;
        _generalService = generalService;
        _districtService = districtService;
        _palikaService = palikaService;
        _provinceService = provinceService;
    }

    [HttpGet, Route("accesslevels")]
    public async Task<IActionResult> GetAllAccessLevels()
    {
        try
        {
            return Ok(await _generalService.GetAllAccessLevels());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting access levels", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("update-district"), CheckAccessForMenu(MenuCode = 5)]
    public async Task<IActionResult> UpdateActiveDistrict(DistrictActiveStatusDto activeStatusDto)
    {
        try
        {
            await _districtService.UpdateActiveStatusOfDistrict(activeStatusDto.Id);
            return Ok("District has been updated");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred updating active district", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("update-palikass"), CheckAccessForMenu(MenuCode = 6)]
    public async Task<IActionResult> UpdateActivePalika(PalikaUpdateDto updateActiveStatus)
    {
        try
        {
            await _palikaService.UpdateActiveStatusOfPalika(updateActiveStatus.Id);
            return Ok("Palika has been updated");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred updating active palika", e.Message);
            return BadRequest(e.Message);
        }
    }
    [HttpGet, Route("districts")]
    public async Task<IActionResult> GetAllDistricts()
    {
        try
        {

            return Ok(await _districtService.GetAllDistrictsForTable());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all districts", e.Message);
            return BadRequest(e.Message);
        }

    }
    [HttpGet, Route("palikas")]
    public async Task<IActionResult> GetAllPalikas()
    {
        try
        {
            return Ok(await _palikaService.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all districts", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("provinces")]
    public async Task<IActionResult> GetAllProvince()
    {
        try
        {
            return Ok(await _provinceService.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all Province", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("update-province"), CheckAccessForMenu(MenuCode = 20)]
    public async Task<IActionResult> UpdateActiveProvince(ProvinceActiveStatusDto activeStatusDto)
    {
        try
        {
            await _provinceService.UpdateActiveStatusOfProvince(activeStatusDto.Id);
            return Ok("Province has been updated");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred updating active district", e.Message);
            return BadRequest(e.Message);
        }
    }


    [HttpGet, Route("address-for-filter")]
    public async Task<IActionResult> GetAddressFilterData()
    {
        try
        {
            AddressFilterDto addressFilterDto = new AddressFilterDto();
            addressFilterDto.Districts = await _districtService.GetAllDistrictForSelection();
            addressFilterDto.Palikas = await _palikaService.GetAllActivePalikas();
            addressFilterDto.Provinces = await _generalService.GetAllProvinces();
            return Ok(addressFilterDto);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting address filter dto", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("months")]
    public async Task<IActionResult> GetAllMonths()
    {
        try
        {
            return Ok(await _generalService.GetAllMonths());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all Months", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("locations")]
    public async Task<IActionResult> GetAllLocation()
    {
        try
        {
            return Ok(await _generalService.GetAllLocations());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all Location", e.Message);
            return BadRequest(e.Message);
        }
    }
}

