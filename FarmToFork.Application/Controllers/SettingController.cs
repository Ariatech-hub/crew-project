
using FarmToFork.Business.Mapper;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Exception;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using OfficeOpenXml.Utils;

namespace FarmToFork.Application.Controllers
{
    [Route("api/setting"), Authorize]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommunityService _communityService;
        private readonly IGrainService _grainService;
        private readonly IInternetService _internetService;
        private readonly IEducationLevelService _educationLevelService;
        private readonly IEthnicityService _ethnicityService;
        private readonly IObstacleService _obstacleService;
        private readonly IIncomeSourceService _incomeSourceService;
        private readonly ILabourDivisionService _labourDivisionService;
        private readonly IOccupationService _occupationService;
        private readonly IParticipantOptionService _participantOptionService;
        private readonly IYearService _yearService;
        private readonly ILocationService _locationService;
        private readonly IMaritalStatusService _maritalStatusService;
        private readonly ICardImageService _cardImageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGeneralUtility _generalUtility;
        private readonly IStatusService _statusService;
        
        public SettingController(ILogger<SettingController> logger, ICommunityService communityService,
            IGrainService grainService, IInternetService internetService,
            IEducationLevelService educationLevelService, IEthnicityService ethnicityService, IObstacleService obstacleService,
            IIncomeSourceService incomeSourceService, ILabourDivisionService labourDivisionService, IOccupationService occupationService,
            IParticipantOptionService participantOptionService, IYearService yearService, ILocationService locationService, IMaritalStatusService maritalStatusService,
            ICardImageService cardImageService, IWebHostEnvironment webHostEnvironment, IGeneralUtility generalUtility,
            IStatusService statusService)
        {
            _logger = logger;
            _communityService = communityService;
            _grainService = grainService;
            _internetService = internetService;
            _educationLevelService = educationLevelService;
            _ethnicityService = ethnicityService;
            _obstacleService = obstacleService;
            _incomeSourceService = incomeSourceService;
            _labourDivisionService = labourDivisionService;
            _occupationService = occupationService;
            _participantOptionService = participantOptionService;
            _yearService = yearService;
            _locationService = locationService;
            _maritalStatusService = maritalStatusService;
            _cardImageService = cardImageService;
            _webHostEnvironment = webHostEnvironment;
            _generalUtility = generalUtility;
            _statusService = statusService;
        }


        #region  --------- Community ----------

        [HttpPost, Route("community/insert"), CheckAccessForMenu(MenuCode = 7)]
        public async Task<IActionResult> InsertCommunity(CommunityInsertDto community)
        {
            try
            {
                CommunityDto insertedCommunity = await _communityService.Insert(community);
                return Ok($"The community {insertedCommunity.Name} has been added");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured during insert communtity.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("community/update"), CheckAccessForMenu(MenuCode = 7)]
        public async Task<IActionResult> UpdateCommunity(CommunityUpdateDto community)
        {
            try
            {
                await _communityService.Update(community);
                return Ok($"The community  has been updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured during community update.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("community/delete"), CheckAccessForMenu(MenuCode = 7)]
        public async Task<IActionResult> DeleteCommunity(CommunityDeleteDto community)
        {
            try
            {
                var deletedCommunity = await _communityService.Delete(community);
                return Ok($"The community {deletedCommunity.Name}  has been delete");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Exception occured during delete community.");
                return BadRequest(ex.Message);

            }
        }

        [HttpGet, Route("communities")]
        public async Task<IActionResult> GetAllCommunities()
        {
            try
            {
                var data = await _communityService.GetAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured during getting all community.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("community/{id}"), CheckAccessForMenu(MenuCode = 7)]
        public async Task<IActionResult> GetCommunityById(int id)
        {
            try
            {
                CommunityDto communityDto = await _communityService.GetById(id);
                return Ok(communityDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occured during getting all community.");
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ------- Education Level -------


        [HttpPost, Route("education-level/insert"), CheckAccessForMenu(MenuCode = 4)]
        public async Task<IActionResult> InsertEducationLevel(EducationLevelInsertDto educationLevelInsertDto)
        {
            try
            {
                var insertedEducationLevel = await _educationLevelService.Insert(educationLevelInsertDto);
                return Ok($"The Education Level {insertedEducationLevel.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting education life", e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpPost, Route("education-level/update"), CheckAccessForMenu(MenuCode = 4)]
        public async Task<IActionResult> UpdateEducationLevel(EducationLevelUpdateDto educationLevelUpdateDto)
        {
            try
            {
                await _educationLevelService.Update(educationLevelUpdateDto);
                return Ok($"The Education Level  has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating education life", e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpGet, Route("education-levels")]
        public async Task<IActionResult> GetAllEducationLevels()
        {
            try
            {
                return Ok(await _educationLevelService.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred during getting all education levels.");
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("education-level/{id}"), CheckAccessForMenu(MenuCode = 4)]
        public async Task<IActionResult> GetEducationLevelById(int id)
        {
            try
            {
                return Ok(await _educationLevelService.GetById(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred during getting all education level by id.");
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("education-level/delete"), CheckAccessForMenu(MenuCode = 4)]
        public async Task<IActionResult> DeleteEducationLevel(EducationLevelDeleteDto educationLevelDeleteDto)
        {
            try
            {
                var deletedEducationLevel = await _educationLevelService.Delete(educationLevelDeleteDto);
                return Ok($"The Education level {deletedEducationLevel.Name} has been deleted");
            }
            catch (Exception e)
            {
                if (e.GetBaseException() is SqlException sqlException && sqlException.Errors.Count > 0 && sqlException.Errors[0].Number == 547)
                {
                    return BadRequest("Education Level is in use");
                }

                _logger.LogError(e, "Exception occurred during deleting education level.");
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region  --------- Ethnicity ---------

        [HttpPost, Route("ethnicity/insert"), CheckAccessForMenu(MenuCode = 8)]
        public async Task<IActionResult> InsertEthnicity(EthnicityInsertDto ethnicityInsertDto)
        {
            try
            {
                var insertedEthnicity = await _ethnicityService.Insert(ethnicityInsertDto);
                return Ok($"Ethnicity {insertedEthnicity.Name} has been inserted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred during inserting ethnicity.");
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("ethnicity/update"), CheckAccessForMenu(MenuCode = 8)]
        public async Task<IActionResult> UpdateEthnicity(EthnicityUpdateDto ethnicityUpdateDto)
        {
            try
            {
                await _ethnicityService.Update(ethnicityUpdateDto);
                return Ok("Ethnicity has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred during updating ethnicity.");
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("ethnicity/{ethnicityId}"), CheckAccessForMenu(MenuCode = 8)]
        public async Task<IActionResult> GetById(int ethnicityId)
        {
            try
            {
                return Ok(await _ethnicityService.GetEthnicityById(ethnicityId));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred getting ethnicity by id.");
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("ethnicity-delete"), CheckAccessForMenu(MenuCode = 8)]
        public async Task<IActionResult> DeleteEthnicity(EthnicityDeleteDto ethnicityDeleteDto)
        {
            try
            {
                var deletedEthnicity = await _ethnicityService.Delete(ethnicityDeleteDto);
                return Ok($"The Ethnicity {deletedEthnicity.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while deleting ethnicity.");
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("ethnicities")]
        public async Task<IActionResult> GetAllEthnicity()
        {
            try
            {
                return Ok(await _ethnicityService.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while deleting ethnicity.");
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region  ------- Grain ------
        [HttpPost, Route("grain/insert"), CheckAccessForMenu(MenuCode = 10)]
        public async Task<IActionResult> InsetGrain(GrainInsertDto grainInsertDto)
        {
            try
            {
                GrainInsertDto insertedGrain = await _grainService.InsertGrain(grainInsertDto);
                return Ok($"Grain {insertedGrain.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting grain ", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("grain/update"), CheckAccessForMenu(MenuCode = 10)]
        public async Task<IActionResult> UpdateGrain(GrainUpdateDto grainUpdateDto)
        {
            try
            {
                await _grainService.UpdateGrain(grainUpdateDto);
                return Ok("Grain has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating grain ", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("grain/delete"), CheckAccessForMenu(MenuCode = 10)]
        public async Task<IActionResult> DeletGrain(GrainDeleteDto grainDeleteDto)
        {
            try
            {
                GrainDeleteDto deletedGrain = await _grainService.DeleteGrain(grainDeleteDto);
                return Ok($"The grain {deletedGrain.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating grain ", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("grains")]
        public async Task<IActionResult> GetAllGrains()
        {
            try
            {
                return Ok(await _grainService.GetAllGrains());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating grain ", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region  -------- Internet- type -------

        [HttpPost, Route("internet-type/insert"), CheckAccessForMenu(MenuCode = 10)]
        public async Task<IActionResult> InsertInternetType(InternetTypeInsertDto internetTypeInsert)
        {
            try
            {
                InternetTypeInsertDto internetTypeInsertDto = await _internetService.InsertInternetTye(internetTypeInsert);
                return Ok($"Internet type {internetTypeInsertDto.Name} has been inserted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting internet Type", e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost, Route("internet-type/update"), CheckAccessForMenu(MenuCode = 10)]
        public async Task<IActionResult> UpdateInternetType(InternetTypeUpdateDto internetTypeUpdateDto)
        {
            try
            {
                await _internetService.UpdateInternetType(internetTypeUpdateDto);
                return Ok("Internet type has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting internet Type", e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost, Route("internet-type/delete"), CheckAccessForMenu(MenuCode = 10)]
        public async Task<IActionResult> DeleteInternetType(InternetTypeDeleteDto internetTypeDeleteDto)
        {
            try
            {
                await _internetService.DeleteInternetType(internetTypeDeleteDto);
                return Ok($"Internet Type {internetTypeDeleteDto.Name}  has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting internet Type", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("internet-types")]
        public async Task<IActionResult> GetAllInternetTypes()
        {
            try
            {
                return Ok(await _internetService.GetAllInternetTypes());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting internet Type", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region  ------- Internet Use -------

        [HttpPost, Route("internet-use/insert"), CheckAccessForMenu(MenuCode = 11)]
        public async Task<IActionResult> InsertInternetUse(InternetUseInsertDto internetUseInsertDto)
        {
            try
            {
                InternetUseInsertDto insertedInternetUse = await _internetService.InsertInternetUse(internetUseInsertDto);
                return Ok($"Internet use {insertedInternetUse.Name} has been inserted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occurred while inserting internet use", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("internet-use/update"), CheckAccessForMenu(MenuCode = 11)]
        public async Task<IActionResult> UpdateInternetUse(InternetUseUpdateDto internetUseUpdateDto)
        {
            try
            {
                await _internetService.UpdateInternetUse(internetUseUpdateDto);
                return Ok("Internet use has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occurred while inserting internet use", e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost, Route("internet-use/delete"), CheckAccessForMenu(MenuCode = 11)]
        public async Task<IActionResult> DeleteInternetUse(InternetUseDeleteDto internetUseDeleteDto)
        {
            try
            {
                var deletedInternetUse = await _internetService.DeleteInternetUse(internetUseDeleteDto);
                return Ok($"Internet use {deletedInternetUse.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occurred while inserting internet use", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("internet-uses")]
        public async Task<IActionResult> GetAllInternetUses()
        {
            try
            {
                return Ok(await _internetService.GetAllInternetUse());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occurred while inserting internet use", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion


        #region  ------ Obstacle -------

        [HttpPost, Route("obstacle/insert"), CheckAccessForMenu(MenuCode = 13)]
        public async Task<IActionResult> InsertObstacle(ObstacleInsertDto obstacleInsertDto)
        {
            try
            {
                ObstacleInsertDto insertedObstacle = await _obstacleService.Insert(obstacleInsertDto);
                return Ok($"The Obstacle {insertedObstacle.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting obstacle", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("obstacle/update"), CheckAccessForMenu(MenuCode = 13)]
        public async Task<IActionResult> UpdateObstacle(ObstacleUpdateDto obstacleUpdateDto)
        {
            try
            {
                await _obstacleService.Update(obstacleUpdateDto);
                return Ok($"The Obstacle has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating obstacle", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("obstacles")]
        public async Task<IActionResult> GetAllObstacles()
        {
            try
            {
                return Ok(await _obstacleService.GetAllObstacles());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all obstacles", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("obstacle/delete"), CheckAccessForMenu(MenuCode = 13)]
        public async Task<IActionResult> DeleteObstacle(ObstacleDeleteDto obstacleDeleteDto)
        {
            try
            {
                ObstacleDeleteDto deletedObstacle = await _obstacleService.Delete(obstacleDeleteDto);
                return Ok($"Obstacle {deletedObstacle.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while deleting obsatcles", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ---------IncomeSource-----------

        //Menu Code ?? 

        [HttpPost, Route("income-source/insert"), CheckAccessForMenu(MenuCode = 18)]
        public async Task<IActionResult> InsertIncomeSource(IncomeSourceInsertDto incomeSourceInsertDto)
        {
            try
            {
                IncomeSourceInsertDto insertedIncomeSource = await _incomeSourceService.Insert(incomeSourceInsertDto);
                return Ok($"The Income Source {insertedIncomeSource.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting Income Source", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("income-source/update"), CheckAccessForMenu(MenuCode = 18)]
        public async Task<IActionResult> UpdateIncomeSource(IncomeSourceUpdateDto incomeSourceUpdateDto)
        {
            try
            {
                await _incomeSourceService.Update(incomeSourceUpdateDto);
                return Ok($"The Income Source has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating Income Source", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("income-source")]
        public async Task<IActionResult> GetAllIncomeSource()
        {
            try
            {
                return Ok(await _incomeSourceService.GetAllIncomeSource());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all Income Source", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("income-source/delete"), CheckAccessForMenu(MenuCode = 18)]
        public async Task<IActionResult> DeleteIncomeSource(IncomeSourceDeleteDto incomeSourceDeleteDto)
        {
            try
            {
                IncomeSourceDeleteDto deletedIncomeSource = await _incomeSourceService.Delete(incomeSourceDeleteDto);
                return Ok($"Income Source {deletedIncomeSource.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while Deleting Income Source", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ------Labour Division-----
        [HttpPost, Route("labour-division/insert"), CheckAccessForMenu(MenuCode = 19)]
        public async Task<IActionResult> InsertLabourDivision(LabourDivisionInsertDto labourDivisionInsertDto)
        {
            try
            {
                LabourDivisionInsertDto insertedlabourDivision = await _labourDivisionService.Insert(labourDivisionInsertDto);
                return Ok($"The Labour Division {insertedlabourDivision.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting Labour Division", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("labour-division/update"), CheckAccessForMenu(MenuCode = 19)]
        public async Task<IActionResult> UpdateLabourDivision(LabourDivisionUpdateDto labourDivisionUpdateDto)
        {
            try
            {
                await _labourDivisionService.Update(labourDivisionUpdateDto);
                return Ok($"The Labour division has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating Laboour division", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("labour-divisions")]
        public async Task<IActionResult> GetAllLabourDivision()
        {
            try
            {
                return Ok(await _labourDivisionService.GetAllLabourDivision());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all Laboour division", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("labour-division/delete"), CheckAccessForMenu(MenuCode = 19)]
        public async Task<IActionResult> DeleteLabourDivision(LabourDivisionDeleteDto labourDivisionDeleteDto)
        {
            try
            {
                LabourDivisionDeleteDto deletedLabourDivision = await _labourDivisionService.Delete(labourDivisionDeleteDto);
                return Ok($"Laboour division {deletedLabourDivision.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while Deleting Labour division", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ----occupation---

        [HttpPost, Route("occupation/insert"), CheckAccessForMenu(MenuCode = 17)]
        public async Task<IActionResult> InsertOccupation(OccupationInsertDto occupationInsertDto)
        {
            try
            {
                OccupationInsertDto insertedOccupation = await _occupationService.Insert(occupationInsertDto);
                return Ok($"The Occupation {insertedOccupation.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting Occupation", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("occupation/update"), CheckAccessForMenu(MenuCode = 17)]
        public async Task<IActionResult> UpdateOccupation(OccupationUpdateDto occupationUpdateDto)
        {
            try
            {
                await _occupationService.Update(occupationUpdateDto);
                return Ok($"The Occupation has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating Occupation", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("occupations")]
        public async Task<IActionResult> GetAllOccupations()
        {
            try
            {
                return Ok(await _occupationService.GetAllOccupations());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all Occupations", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("occupation/delete"), CheckAccessForMenu(MenuCode = 17)]
        public async Task<IActionResult> Delete(OccupationDeleteDto occupationDeleteDto)
        {
            try
            {
                OccupationDeleteDto deletedOccupation = await _occupationService.Delete(occupationDeleteDto);
                return Ok($"Occupation {deletedOccupation.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while deleting obsatcles", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion


        #region ------ Participant Option-------


        [HttpPost, Route("participation-option/insert"), CheckAccessForMenu(MenuCode = 21)]
        public async Task<IActionResult> InsertParticipantOption(ParticipantOptionInsertDto participantOptionInsertDto)
        {
            try
            {
                ParticipantOptionInsertDto insertedParticipationOption = await _participantOptionService.Insert(participantOptionInsertDto);
                return Ok($"Participant Option {insertedParticipationOption.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting Income Source", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("participation-option/update"), CheckAccessForMenu(MenuCode = 21)]
        public async Task<IActionResult> UpdateParticipantOption(ParticipantOptionUpdateDto participantOptionUpdateDto)
        {
            try
            {
                await _participantOptionService.Update(participantOptionUpdateDto);
                return Ok($"The Participation Option has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating Participant Option", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("participation-options")]
        public async Task<IActionResult> GetAllParticipantOption()
        {
            try
            {
                return Ok(await _participantOptionService.GetAllParticipantOption());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all Income Source", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("participation-option/delete"), CheckAccessForMenu(MenuCode = 21)]
        public async Task<IActionResult> DeleteParticipantOption(ParticipantOptionDeleteDto participantOptionDeleteDto)
        {
            try
            {
                ParticipantOptionDeleteDto deletedParticipationOption = await _participantOptionService.Delete(participantOptionDeleteDto);
                return Ok($"Participation {deletedParticipationOption.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while Deleting Participation Option", e.Message);
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region ------Year----------
        [HttpPost, Route("year/insert"), CheckAccessForMenu(MenuCode = 23)]
        public async Task<IActionResult> InsertYear(YearInsertDto yearInsertDto)
        {
            try
            {
                YearInsertDto insertedYear = await _yearService.Insert(yearInsertDto);
                return Ok($"Year {insertedYear.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting Year", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("year/update"), CheckAccessForMenu(MenuCode = 23)]
        public async Task<IActionResult> UpdateYear(YearUpdateDto yearUpdateDto)
        {
            try
            {
                await _yearService.Update(yearUpdateDto);
                return Ok($"The Year {yearUpdateDto.Name} has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating Year", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("years")]
        public async Task<IActionResult> GetAllYear()
        {
            try
            {
                return Ok(await _yearService.GetAllYear());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all Year", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("year/delete")]
        public async Task<IActionResult> DeleteYear(YearDeleteDto yearDeleteDto)
        {
            try
            {
                YearDeleteDto deletedYear = await _yearService.Delete(yearDeleteDto);
                return Ok($"Year {deletedYear.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while Deleting Year", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion


        #region --------Location ---------
        [HttpPost, Route("location/insert")]
        public async Task<IActionResult> Insertlocation(LocationInsertDto locationInsertDto)
        {
            try
            {
                LocationInsertDto insertedlocation = await _locationService.Insert(locationInsertDto);
                return Ok($"location {insertedlocation.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting location", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("location/update")]
        public async Task<IActionResult> Updatelocation(LocationUpdateDto locationUpdateDto)
        {
            try
            {
                await _locationService.Update(locationUpdateDto);
                return Ok($"The location {locationUpdateDto.Name} has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating location", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("locations")]
        public async Task<IActionResult> GetAlllocation()
        {
            try
            {
                return Ok(await _locationService.GetAllLocation());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all location", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("location/delete")]
        public async Task<IActionResult> Deletelocation(LocationDeleteDto locationDeleteDto)
        {
            try
            {
                LocationDeleteDto deletedLocation = await _locationService.Delete(locationDeleteDto);
                return Ok($"Location {deletedLocation.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while Deleting location", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ------Marital Status---------
        [HttpPost, Route("marital-status/insert")]
        public async Task<IActionResult> InsertMaritalStatus(MaritalStatusInsertDto maritalStatusInsertDto)
        {
            try
            {
                MaritalStatusInsertDto insertedMaritalStatus = await _maritalStatusService.Insert(maritalStatusInsertDto);
                return Ok($"MaritalStatus {insertedMaritalStatus.Name} has been added");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while inserting MaritalStatus", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("marital-status/update")]
        public async Task<IActionResult> UpdateMaritalStatus(MaritalStatusUpdateDto maritalStatusUpdateDto)
        {
            try
            {
                await _maritalStatusService.Update(maritalStatusUpdateDto);
                return Ok($"The Marital Status {maritalStatusUpdateDto.Name} has been updated");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while updating MaritalStatus", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("marital-statuses")]
        public async Task<IActionResult> GetAllMaritalStatus()
        {
            try
            {
                return Ok(await _maritalStatusService.GetAllMaritalStatus());
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while getting all MaritalStatus", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("marital-status/delete")]
        public async Task<IActionResult> DeleteMaritalStatus(MaritalStatusDeleteDto maritalStatusDeleteDto)
        {
            try
            {
                MaritalStatusDeleteDto deletedMaritalStatus = await _maritalStatusService.Delete(maritalStatusDeleteDto);
                return Ok($"Marital Status {deletedMaritalStatus.Name} has been deleted");
            }
            catch (Exception e)
            {
                _logger.LogError("Exception Occurred while Deleting MaritalStatus", e.Message);
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region ------Card Image---------
        [HttpGet, Route("card-images")]
        public async Task<IActionResult> GetCardImages()
        {
            try
            {
                IEnumerable<CardImageDto> cardImages = await _cardImageService.GetCardImages();
                foreach(var item in cardImages)
                {
                    item.FilePath = $"{Request.Scheme}://{Request.Host}/Images/{item.FileName}";
                }
                return Ok(cardImages);
            }
            catch(Exception e)
            {
                _logger.LogError("Exception occurred while getting card images", e.Message);
                return BadRequest(e.Message);
            }
        }
        [HttpPost, Route("card-image/add")]
        public async Task<IActionResult> AddCardImage()
        {
            string imageUploadPath = _webHostEnvironment.ContentRootPath + "\\Images";
            _generalUtility.CreateFolderIfNotExist(imageUploadPath);
            try
            {
                if (Request.Form.Files.Any())
                {
                    string ext = Path.GetExtension(Request.Form.Files[0].FileName);
                    Path.GetFileNameWithoutExtension(Request.Form.Files[0].FileName);
                    string savedFileName = DateTime.UtcNow.AddMinutes(345).ToString("yyyyMMddHHmmssffff") + ext;
                    string filePath = Path.Combine(imageUploadPath, savedFileName);
                    await using var stream = System.IO.File.Create(filePath);
                    await Request.Form.Files[0].CopyToAsync(stream);
                    CardImageInsertDto cardImage = new ()
                    {
                        Name = HttpContext.Request.Form["Title"],
                        FileName = savedFileName
                    };
                    await _cardImageService.AddCardImage(cardImage);
                    return Ok("File has been uploaded");
                }
                else
                {
                    throw new DataValidationException("File is required");
                }
               
            }
            catch(Exception e)
            {
                _logger.LogError("Exception occurred while inserting card images", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("card-image/update")]
        public async Task<IActionResult> UpdateCardImage()
        {
            string imageUploadPath = _webHostEnvironment.ContentRootPath + "\\Images";
            try
            {
                if(Request.Form.Files.Any())
                {
                    if (HttpContext.Request.Form["Id"].FirstOrDefault() == null)
                    {
                        throw new DataNotFoundException("Id is required");
                    }

                    CardImageDto cardImageToBeDeleted = await _cardImageService.GetCardImageById(int.Parse(HttpContext.Request.Form["Id"].Single()));
                    if (!string.IsNullOrEmpty(cardImageToBeDeleted.FileName))
                    {
                        _generalUtility.DeleteFileFromServer(imageUploadPath, cardImageToBeDeleted.FileName);
                    }
                   
                    string ext = Path.GetExtension(Request.Form.Files[0].FileName);
                    Path.GetFileNameWithoutExtension(Request.Form.Files[0].FileName);
                    string savedFileName = DateTime.UtcNow.AddMinutes(345).ToString("yyyyMMddHHmmssffff") + ext;
                    string filePath = Path.Combine(imageUploadPath, savedFileName);
                    await using var stream = System.IO.File.Create(filePath);
                    await Request.Form.Files[0].CopyToAsync(stream);
                    CardImageDto cardImage = new ()
                    {
                        Id = int.Parse(HttpContext.Request.Form["Id"]),
                        Name = HttpContext.Request.Form["Title"],
                        FileName = savedFileName
                    };
                    await _cardImageService.UpdateCardImage(cardImage);
                    return Ok("Card Image has been updated");
                }
                else
                {
                    throw new DataValidationException("File is required");
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occured while updating card images", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("card-image/delete")]
        public async Task<IActionResult> DeleteCardImage(CardImageDeleteDto cardImageDeleteDto)
        {
            try
            {
                CardImageDto deletedCardImage = await _cardImageService.DeleteCardImage(cardImageDeleteDto);
                return Ok(deletedCardImage);

            }
            catch(Exception e)
            {
                _logger.LogError("Exception occured while deleting card image", e.Message);
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ------Status------
        [HttpGet, Route("statuses")]
        public async Task<IActionResult> GetStatues()
        {
            try
            {
                IEnumerable<StatusDto> statuses = await _statusService.GetAllStatus();
                return Ok(statuses);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occurred while getting statuses {e}", e);
                return BadRequest(e.Message);
            }
        } 
        #endregion

    }
}
