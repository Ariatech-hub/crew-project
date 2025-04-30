

using FarmToFork.Business.Enums;
using FarmToFork.Core.Exception;
using FarmToFork.Core.Repositories;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FarmToFork.Application.Controllers;

[Route("api")]
[ApiController]
public class MobileController : ControllerBase
{
    private readonly ILogger<MobileController> _logger;
    private readonly IMobileService _mobileService;
    private readonly IFarmerService _farmerService;
    private readonly ISqlLiteRepository _sqliteRepository;
    private readonly IProductionPlanService _productionPlanService;


    public MobileController(ILogger<MobileController> logger, IMobileService mobileService, IFarmerService farmerService, ISqlLiteRepository sqliteRepository, IProductionPlanService productionPlanService)
    {
        _logger = logger;
        _mobileService = mobileService;
        _farmerService = farmerService;
        _sqliteRepository = sqliteRepository;
        _productionPlanService = productionPlanService;
    }

    [HttpGet, Route("initial-data"), Authorize(Roles = $"{RoleTypeEnum.Enumerator}, {RoleTypeEnum.Cooperative}")]
    public async Task<IActionResult> GetInitialData()
    {
        try
        {
            return Ok(await _mobileService.GetInitialsData());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting initial data ", e.Message);
            return BadRequest(e.Message);
        }
    }


    [HttpPost, Route("farmer/sync"), Authorize(Roles = $"{RoleTypeEnum.Enumerator}, {RoleTypeEnum.Cooperative}")]
    public async Task<IActionResult> FarmerSync()
    {
        try
        {
            var data = HttpContext.Request.Form["data"];
            await _sqliteRepository.InsertResponse(data, "Farmer-Sync");
            IFormFile farmerImage = Request.Form.Files.FirstOrDefault(x => x.Name == "farmerPhoto") ?? throw new DataValidationException("Image is Required");
            var insertedFarmer = await _farmerService.FarmerSync(data, farmerImage);
            return Ok(insertedFarmer);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while farmer data  sync ", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("production-plan-sync"), Authorize(Roles = $"{RoleTypeEnum.Enumerator}, {RoleTypeEnum.Cooperative}")]
    public async Task<IActionResult> FarmerProductionPlanSync(FarmerProductionSyncDto farmerProductionSyncDto)
    {
        try
        {
            
            await _sqliteRepository.InsertResponse(JsonSerializer.Serialize(farmerProductionSyncDto), "Production-plan-sync");
            return Ok(await _farmerService.FarmerProductionPlanSync(farmerProductionSyncDto));

        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting initial data ", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("research-data-sync"), Authorize(Roles = $"{RoleTypeEnum.Enumerator}")]
    public async Task<IActionResult> ResearchDataSync(ResearchDataSyncDto researchDataSyncDto)
    {
        try
        {
            await _sqliteRepository.InsertResponse(JsonSerializer.Serialize(researchDataSyncDto), "Research-sync");
            return Ok(await _mobileService.ResearchDataSync(researchDataSyncDto));
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting initial data ", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("last-season-production-sync"), Authorize(Roles = $"{RoleTypeEnum.Enumerator}")]
    public async Task<IActionResult> LastSeasonProductionSync(LastSeasonSyncDto lastSeasonSyncDto)
    {
        try
        {
            
            await _sqliteRepository.InsertResponse(JsonSerializer.Serialize(lastSeasonSyncDto), "Last-season-production-sync");
            int insertedLastSeasonProductionId = await _mobileService.LastSeasonProductionSync(lastSeasonSyncDto);
            return Ok(insertedLastSeasonProductionId);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while last season Production Sync ", e.Message);
            return BadRequest(e.Message);
        }
    }



    #region  ------ Production Plan  ----------


    [HttpGet, Route("farmer/list/production-plans"), Authorize(Roles = $"{RoleTypeEnum.Cooperative}")]
    public async Task<IActionResult> GetAllFarmerProductionPlanCurrentGrainCycle()
    {
        try
        {
            IEnumerable<ProductionPlanDto> productionPlans = await _productionPlanService.GetAll();
            return Ok(productionPlans);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("farmer/process/production-plan") , Authorize(Roles = $"{RoleTypeEnum.Cooperative}")]
    public async Task<IActionResult> ProcessSaleFarmer(List<ProductionPlanDto> productionPlanDtos)
    {
        try
        {
            await _productionPlanService.ProcessSale(productionPlanDtos);
            return Ok("Added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("farmer/non-member/process/production-plan"), Authorize(Roles = $"{RoleTypeEnum.Cooperative}")]
    public async Task<IActionResult> FarmerNonMemberSync(FarmerNonMemberProductionPlanDto farmerNonMemberProductionPlanDto)
    {
        try
        {
            await _productionPlanService.FarmerNonMemberSync(farmerNonMemberProductionPlanDto);
            return Ok("Added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }




    #endregion
}

