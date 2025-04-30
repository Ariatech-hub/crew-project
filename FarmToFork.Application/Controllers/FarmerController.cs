using FarmToFork.Core.Exception;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Microsoft.AspNetCore.Hosting;

namespace FarmToFork.Application.Controllers;

[Route("api/farmer")]
[ApiController, Authorize]
public class FarmerController : ControllerBase
{
    private readonly IFarmerService _farmerService;
    private readonly ILogger<FarmerController> _logger;

    public FarmerController(IFarmerService farmerService, ILogger<FarmerController> logger)
    {
        _farmerService = farmerService;
        _logger = logger;
    }

    [HttpGet, Route("~/api/farmers"), CheckAccessForMenu(MenuCode = 14)]
    public async Task<IActionResult> GetAllFarmers()
    {
        try
        {
            IEnumerable<FarmerTableDto> farmers = await _farmerService.GetAllFarmerList();
            return Ok(farmers);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception {msg} Occurred while getting all farmer list", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("change-status"), CheckAccessForMenu(MenuCode = 14)]
    public async Task<IActionResult> FarmerActiveStatusChanged(FarmerActiveStatusChangeDto farmerActiveStatusChangeDto)
    {
        try
        {
            var (_, message) = await _farmerService.FarmerActiveStatusChanged(farmerActiveStatusChangeDto.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all farmer list", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("view/{farmerId}"), CheckAccessForMenu(MenuCode = 14)]
    public async Task<IActionResult> GetFarmerView(int farmerId)
    {
        try
        {
            FarmerViewDto farmerViewDto = await _farmerService.GetFarmerForView(farmerId);
            farmerViewDto.PhotoPath = !string.IsNullOrEmpty(farmerViewDto.PhotoName) ? $"{Request.Scheme}://{Request.Host}/Images/Farmer/{farmerViewDto.PhotoName}" : null;
            return Ok(farmerViewDto);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting  farmer view", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("change-picture")]
    public async Task<IActionResult> ChangePicture()
    {
        try
        {
            if (Request.Form.Files.Count == 0)
            {
                throw new  DataValidationException("File is required");
            }
            IFormFile file = Request.Form.Files[0];
            int farmerId = int.Parse(HttpContext.Request.Form["farmerId"]);
            var (photoName, imageUploadPath) = await _farmerService.ChangePicture(file, farmerId);
            if (!string.IsNullOrEmpty(photoName) && System.IO.File.Exists(Path.Join(imageUploadPath , photoName)))
            {
                System.IO.File.Delete(Path.Join(imageUploadPath , photoName));
            }
            return Ok("Image has been changed");

        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while inserting  farmer Picture", e.Message);
            return BadRequest(e.Message);
        }
    }


    [HttpGet, Route("~/api/farmers/card"), CheckAccessForMenu(MenuCode = 14)]
    public async Task<IActionResult> GetFarmerForCard()
    {
        try
        {
            IEnumerable<FarmerTableDto> farmers = await _farmerService.GetAllFarmerList();
            foreach(var farmer in farmers)
            {
                farmer.IsSelected = false;
            }
            return Ok(farmers);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting all farmer list", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("~/api/farmers/production-plan")]
    public async Task<IActionResult> GetFarmerProductionPlan()
    {
        try
        {
            var data = await _farmerService.GetFarmerProductionPlanList();
            return Ok(data);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //TODO: Code Review

    [HttpPost, Route("change-non-member-status")]
    public async Task<IActionResult> UpdateIsNonMember(FarmerActiveStatusChangeDto farmerDto)
    {
        try
        {
            FarmerTableDto farmer = await _farmerService.UpdateIsNonMember(farmerDto.Id);
            return Ok($"The farmer {farmer.FullName} has been updated");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }



}

