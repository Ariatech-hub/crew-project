using FarmToFork.Core.Exception;

namespace FarmToFork.Business.Services
{

    public interface IMobileService
    {
        Task<InitialDataDto> GetInitialsData();

        Task<int> ResearchDataSync(ResearchDataSyncDto researchDataSyncDto);

        Task<int> LastSeasonProductionSync(LastSeasonSyncDto lastSeasonSyncDto);

    }
    public class MobileService : IMobileService
    {

        private readonly IOccupationService _occupationService;
        private readonly IEducationLevelService _educationLevelService;
        private readonly IRelationService _relationService;
        private readonly IDistrictService _districtService;
        private readonly IPalikaService _palikaService;
        private readonly IGenderService _genderService;
        private readonly IGeneralService _generalService;
        private readonly IEthnicityService _ethnicityService;
        private readonly ICommunityService _communityService;
        private readonly IFarmerService _farmerService;
        private readonly IGrainService _grainService;
        private readonly IParticipantOptionService _participantOptionService;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IInternetService _internetService;
        private readonly IObstacleService _obstacleService;
        private readonly IGeneralUtility _generalUtility;

        public MobileService(IOccupationService occupationService, IEducationLevelService educationLevelService,
            IRelationService relationService, IDistrictService districtService, IPalikaService palikaService, IGenderService genderService, IGeneralService generalService,
            IEthnicityService ethnicityService, ICommunityService communityService, IFarmerService farmerService, IGrainService grainService,
            IParticipantOptionService participantOptionService, IFarmerRepository farmerRepository,
            IInternetService internetService, IObstacleService obstacleService, IGeneralUtility generalUtility)
        {

            _occupationService = occupationService;
            _educationLevelService = educationLevelService;
            _relationService = relationService;
            _districtService = districtService;
            _palikaService = palikaService;
            _genderService = genderService;
            _generalService = generalService;
            _ethnicityService = ethnicityService;
            _communityService = communityService;
            _farmerService = farmerService;
            _grainService = grainService;
            _participantOptionService = participantOptionService;
            _farmerRepository = farmerRepository;
            _internetService = internetService;
            _obstacleService = obstacleService;
            _generalUtility = generalUtility;
        }

        public async Task<InitialDataDto> GetInitialsData()
        {
            InitialDataDto initialDataDto = new()
            {
                Occupations = await _occupationService.GetActiveOccupations(),
                EducationLevels = await _educationLevelService.GetAllActiveEducationLevels(),
                Relationships = await _relationService.GetAllActiveRelations(),
                Districts = await _districtService.GetAllActiveDistricts(),
                Palikas = await _palikaService.GetAllActivePalikas(),
                Genders = await _genderService.GetAllActiveGenders(),
                Locations = await _generalService.GetAllLocations(),
                Provinces = (await _generalService.GetAllProvinces()).Where(x => x.IsActive),
                Ethnicities = await _ethnicityService.GetAll(),
                Communities = await _communityService.GetAll(),
                MaritalStatus = await _generalService.GetAllMartialStatus(),
                Grains = await _grainService.GetAllActiveGrains(),
                ParticipantOptions = await _participantOptionService.GetAllActiveParticipantOptions(),
                InternetTypes = await _internetService.GetAllActiveInternetTypes(),
                InternetUse = await _internetService.GetAllActiveInternetUse(),
                Obstacles = await _obstacleService.GetAllActiveObstacles(),
                LaborDivisions = await _generalService.GetAllLaborDivisions(),
                Months = await _generalService.GetAllMonths(),
                GrainCycles = await _grainService.GetAllActiveGrainCycles(),
                Participants = await _farmerService.GetFarmerHavingMembershipId(),
                Farmers = await _farmerService.GetAllFarmerList()
            };

            return initialDataDto;
        }

        public async Task<int> ResearchDataSync(ResearchDataSyncDto researchDataSyncDto)
        {
            DateTime createdDateMobile = _generalUtility.ConvertEpochDateTime(researchDataSyncDto.ResearchData.CreatedDate!.Value);
            researchDataSyncDto.ResearchData.CreatedDate = null;

            Research mappedResearch = ObjectMapper.Mapper.Map<Research>(researchDataSyncDto.ResearchData);
            if (researchDataSyncDto.Farmer is null || researchDataSyncDto.Farmer.SyncId is 0 or null)
            {
                throw new DataValidationException("Invalid Farmer Data");
            }
            mappedResearch.FarmerId = researchDataSyncDto.Farmer.SyncId.Value;
            mappedResearch.CreatedDate = createdDateMobile;
            mappedResearch.SyncDate = _generalUtility.GetCurrentNepalTime();
            mappedResearch.CreatedBy = researchDataSyncDto.Enumerator.Username;



            if (researchDataSyncDto.Farmer is null)
            {
                throw new DataValidationException("Invalid Farmer");
            }

            if (researchDataSyncDto.Farmer.SyncId is 0 or null)
            {
                throw new DataValidationException("Invalid FarmerId");
            }


            if (researchDataSyncDto.FarmerMemberAgricultureParticipantData.Any())
            {
                foreach (var agricultureData in researchDataSyncDto.FarmerMemberAgricultureParticipantData)
                {
                    if (agricultureData.FarmerMemberId == 0)
                    {
                        throw new DataValidationException("Invalid Farmer Member");
                    }
                    agricultureData.FarmerMemberId = await _farmerRepository.GetFarmerMemberIdByFarmerAndMemberSyncId(researchDataSyncDto.Farmer.SyncId.Value, agricultureData.FarmerMemberId);
                    agricultureData.SyncId = agricultureData.Id;
                    agricultureData.SyncDate = _generalUtility.GetCurrentNepalTime();
                    agricultureData.Id = 0;
                    agricultureData.ResearchId = 0;
                    agricultureData.CreatedDate = agricultureData.SyncDate;


                }
            }

            IEnumerable<FarmerMemberAgricultureParticipant> mappedFarmerMemberAgricultureParticipants =
                ObjectMapper.Mapper.Map<IEnumerable<FarmerMemberAgricultureParticipant>>(researchDataSyncDto
                    .FarmerMemberAgricultureParticipantData);

            var farmerMemberAgricultureParticipants = mappedFarmerMemberAgricultureParticipants.ToList();
            if (farmerMemberAgricultureParticipants.Any())
            {
                foreach (var item in farmerMemberAgricultureParticipants)
                {
                    item.FarmerMember = null;
                    item.ParticipantOption = null;
                    item.Research = null;
                }

            }


            if (researchDataSyncDto.MarketStatusData.Any())
            {
                foreach (var marketStatus in researchDataSyncDto.MarketStatusData)
                {

                    marketStatus.SyncId = marketStatus.Id;
                    marketStatus.SyncDate = _generalUtility.GetCurrentNepalTime();
                    marketStatus.CreatedDate = marketStatus.SyncDate;
                    marketStatus.Id = 0;
                    marketStatus.ResearchId = 0;
                }
            }

            List<ResearchInternetUse> researchInternetUses = new();

            if (!string.IsNullOrEmpty(researchDataSyncDto.ResearchData.UseOfInternetIds))
            {
                var internetUseIds = researchDataSyncDto.ResearchData.UseOfInternetIds.Split(",");
                foreach (var internetUseId in internetUseIds)
                {
                    researchInternetUses.Add(new ResearchInternetUse()
                    {
                        Id = 0,
                        ResearchId = 0,
                        UseOfInternetId = int.Parse(internetUseId)
                    });
                }

            }

            List<ResearchObstacle> researchObstacles = new();
            if (!string.IsNullOrEmpty(researchDataSyncDto.ResearchData.ProductionObstacleIds))
            {
                var productionObstacle = researchDataSyncDto.ResearchData.ProductionObstacleIds.Split(",");
                foreach (var internetUseId in productionObstacle)
                {
                    researchObstacles.Add(new ResearchObstacle()
                    {
                        Id = 0,
                        IsProduction = true,
                        IsSale = false,
                        ObstacleId = int.Parse(internetUseId)

                    });
                }

            }
            if (!string.IsNullOrEmpty(researchDataSyncDto.ResearchData.SaleObstacleIds))
            {
                var salesObstacle = researchDataSyncDto.ResearchData.SaleObstacleIds.Split(",");
                foreach (var internetUseId in salesObstacle)
                {
                    researchObstacles.Add(new ResearchObstacle()
                    {
                        Id = 0,
                        IsProduction = false,
                        IsSale = true,
                        ObstacleId = int.Parse(internetUseId)

                    });
                }

            }

            IEnumerable<MarketStatus> mappedMarketStatus = ObjectMapper.Mapper.Map<IEnumerable<MarketStatus>>(researchDataSyncDto.MarketStatusData);

            foreach (var item in mappedMarketStatus)
            {

                item.Grain = null;
                item.Month = null;
                item.Research = null;
            }

            mappedResearch.FarmerMemberAgricultureParticipants = farmerMemberAgricultureParticipants.ToList();
            mappedResearch.MarketStatuses = mappedMarketStatus.ToList();
            mappedResearch.ResearchInternetUses = researchInternetUses;
            mappedResearch.Id = 0;
            mappedResearch.ResearchObstacles = researchObstacles;
            mappedResearch.Farmer = null;
            mappedResearch.HarvestingAndStoringAfterBeanProductionLabourDivision = null;
            mappedResearch.LandOwnershipForBeanProductionLabourDivision = null;
            mappedResearch.MakingLandReadyForBeanProductionLabourDivision = null;
            mappedResearch.PlantSeedForBeanProductionLabourDivision = null;
            mappedResearch.SellingAndMarketingAfterBeanProductionLabourDivision = null;
            mappedResearch.TakingCareForBeanProductionLabourDivision = null;
            mappedResearch.TypeOfInternet = null;

            var insertedResearchId = await _farmerRepository.ResearchDataSync(mappedResearch, researchDataSyncDto.Farmer.SyncId ?? 0);
            return insertedResearchId;
        }

        public async Task<int> LastSeasonProductionSync(LastSeasonSyncDto lastSeasonSyncDto)
        {

            if (lastSeasonSyncDto is null)
            {
                throw new DataValidationException("Invalid Last season production");
            }

            if (lastSeasonSyncDto.LastSeasonProductionData is null)
            {
                throw new DataValidationException("Invalid Last season production");
            }

            if (lastSeasonSyncDto.Farmer.SyncId is 0 or null)
            {
                throw new DataValidationException("Invalid Farmer");
            }

            if (await _farmerRepository.CheckIfLastSeasonAlreadyExists(lastSeasonSyncDto.Farmer.SyncId.Value, lastSeasonSyncDto.LastSeasonProductionData.GrainId))

            {
                throw new DataValidationException("Data has already been synced");
            }

            LastSeasonProductionDto lastSeasonProductionDto = lastSeasonSyncDto.LastSeasonProductionData;

            LastSeasonProduction mappedLastSeasonProduction = ObjectMapper.Mapper.Map<LastSeasonProduction>(lastSeasonProductionDto);
            mappedLastSeasonProduction.CreatedDate = _generalUtility.GetCurrentNepalTime();
            mappedLastSeasonProduction.CreatedBy = lastSeasonSyncDto.Enumerator.Username;
            mappedLastSeasonProduction.GrainId = lastSeasonSyncDto.LastSeasonProductionData.GrainId;
            mappedLastSeasonProduction.Id = 0;
            mappedLastSeasonProduction.FarmerId = lastSeasonSyncDto.Farmer.SyncId.Value;
            mappedLastSeasonProduction.SyncId = lastSeasonSyncDto.LastSeasonProductionData.Id;
            return await _farmerRepository.LastSeasonProductionSync(mappedLastSeasonProduction);
        }
    }



}
