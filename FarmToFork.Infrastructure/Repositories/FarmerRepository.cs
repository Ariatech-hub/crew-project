

using FarmToFork.Core.Exception;
using Newtonsoft.Json;

namespace FarmToFork.Infrastructure.Repositories;

public class FarmerRepository : IFarmerRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IConnectionFactory _connectionFactory;

    public FarmerRepository(AppDbContext appDbContext, IConnectionFactory connectionFactory)
    {
        _appDbContext = appDbContext;
        _connectionFactory = connectionFactory;
    }

    public Task<Farmer> AddAsync(Farmer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Farmer> DeleteAsync(Farmer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> FarmerSync(Farmer farmer, DataTable farmerMemberTable, DataTable farmerMemberPropertyTable, DataTable farmerMemberTransactionTable)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        const string query = "usp_Farmer_Sync";
        var param = new DynamicParameters();
        param.Add("@FirstName", farmer.FirstName);
        param.Add("@Id", farmer.Id);
        param.Add("@MiddleName", farmer.MiddleName);
        param.Add("@LastName", farmer.LastName);
        param.Add("@GenderId", farmer.GenderId);
        param.Add("@EthnicityId", farmer.EthnicityId);
        param.Add("@MartialStatusId", farmer.MaritalStatusId);
        param.Add("@DateOfBirth", farmer.DateOfBirth);
        param.Add("@ProvinceId", farmer.ProvinceId);
        param.Add("@DistrictId", farmer.DistrictId);
        param.Add("@PalikaId", farmer.PalikaId);
        param.Add("@Ward", farmer.Ward);
        param.Add("@Tole", farmer.Tole);
        param.Add("@PhotoName", farmer.PhotoName);
        param.Add("@MobileNumber", farmer.MobileNumber);
        param.Add("@Latitude", farmer.Latitude);
        param.Add("@Longitude", farmer.Longitude);
        param.Add("@CreatedBy", farmer.CreatedBy);
        param.Add("@CreatedDate", farmer.CreatedDate);
        param.Add("@CommunityId", farmer.CommunityId);
        param.Add("@HusbandOrFatherName", farmer.HusbandOrFatherName);
        param.Add("@OccupationId", farmer.OccupationId);
        param.Add("@EducationLevelId", farmer.EducationLevelId);
        param.Add("@Age", farmer.Age);
        param.Add("@CitizenshipNumber", farmer.CitizenshipNumber);
        param.Add("@OwnSmartPhone", farmer.OwnSmartPhone);
        param.Add("@GrandFatherOrFatherInLawName", farmer.GrandFatherOrFatherInLawName);
        param.Add("@HouseValuationAmount", farmer.HouseValuationAmount);
        param.Add("@BusinessValuationAmount", farmer.BusinessValuationAmount);
        param.Add("@JewelryValuationAmount", farmer.JewelryValuationAmount);
        param.Add("@NomineeName", farmer.NomineeName);
        param.Add("@NomineeRelationshipId", farmer.NomineeRelationshipId);
        param.Add("@CattleValuation", farmer.CattleValuationAmount);
        param.Add("@BankBalanceValuation", farmer.BankBalanceValuationAmount);
        param.Add("@OtherValuationAmount", farmer.OtherValuationAmount);
        param.Add("@Identifier", farmer.Identifier);
        param.Add("@FarmerMembershipId", farmer.FarmerMembershipNumber);
        param.Add("@HusbandOrWifeName", farmer.HusbandOrWifeName);
        param.Add("@IsNonMember", farmer.IsNonMember);
        param.Add("@FarmerMemberTable", farmerMemberTable.AsTableValuedParameter("FarmerMemberTable"));
        param.Add("@FarmerMemberPropertyTable", farmerMemberPropertyTable.AsTableValuedParameter("FarmerMemberPropertyTable"));
        param.Add("@FarmerMemberTransactionTable", farmerMemberTransactionTable.AsTableValuedParameter("FarmerMemberTransactionTable"));
        return await conn.QueryFirstOrDefaultAsync<int>(query, param, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Farmer>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Farmers
                .Include(x => x.Province)
                .Include(x => x.District)
                .Include(x => x.Palika)
                .Include(x => x.Community)
                .Include(x => x.Gender)
                .AsNoTracking().OrderByDescending(x => x.Id)
                .ToListAsync()
            : await _appDbContext.Farmers.ToListAsync();

    }

    public Task<IEnumerable<Farmer>> GetAsync(Expression<Func<Farmer, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<Farmer> GetByIdAsync(int id)
    {
        var farmer = await _appDbContext
            .Farmers
            .Include(x => x.Gender)
            .Include(x => x.Ethnicity)
            .Include(x => x.MaritalStatus)
            .Include(x => x.Province)
            .Include(x => x.District)
            .Include(x => x.Palika)
            .Include(x => x.Community)
            .Include(x => x.Occupation)
            .Include(x => x.EducationLevel)
            .Include(x => x.NomineeRelationship)
            .Include(x => x.FarmerMembers)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.Relation)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.Occupation)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.EducationLevel)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.Gender)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.FarmerMemberProperties)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.FarmerMemberProperties).ThenInclude(x => x.District)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.FarmerMemberProperties).ThenInclude(x => x.Palika)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.FarmerMemberProperties).ThenInclude(x => x.Province)
            .Include(x => x.FarmerMembers).ThenInclude(x => x.FarmerMemberTransactions)
            .Include(x => x.LastSeasonProductions)
            .Include(x => x.LastSeasonProductions).ThenInclude(x => x.Grain)
            .Include(x => x.ProductionPlans)
            .Include(x => x.ProductionPlans).ThenInclude(x => x.Grain)
            .Include(x => x.ProductionPlans).ThenInclude(x => x.GrainCycle)
            .Include(x => x.Researches)
            .Include(x => x.Researches).ThenInclude(x => x.TypeOfInternet)
            .Include(x => x.Researches).ThenInclude(x => x.MakingLandReadyForBeanProductionLabourDivision)
            .Include(x => x.Researches).ThenInclude(x => x.PlantSeedForBeanProductionLabourDivision)
            .Include(x => x.Researches).ThenInclude(x => x.TakingCareForBeanProductionLabourDivision)
            .Include(x => x.Researches).ThenInclude(x => x.HarvestingAndStoringAfterBeanProductionLabourDivision)
            .Include(x => x.Researches).ThenInclude(x => x.SellingAndMarketingAfterBeanProductionLabourDivision)
            .Include(x => x.Researches).ThenInclude(x => x.LandOwnershipForBeanProductionLabourDivision)
            .Include(x => x.Researches).ThenInclude(x => x.ResearchInternetUses)
            .Include(x => x.Researches).ThenInclude(x => x.ResearchInternetUses).ThenInclude(x => x.UseOfInternet)
            .Include(x => x.Researches).ThenInclude(x => x.ResearchObstacles)
            .Include(x => x.Researches).ThenInclude(x => x.ResearchObstacles).ThenInclude(x => x.Obstacle)
            .Include(x => x.Researches).ThenInclude(x => x.FarmerMemberAgricultureParticipants)
            .Include(x => x.Researches).ThenInclude(x => x.MarketStatuses)
            .Include(x => x.Researches).ThenInclude(x => x.MarketStatuses).ThenInclude(x => x.Grain)
            .Include(x => x.Researches).ThenInclude(x => x.MarketStatuses).ThenInclude(x => x.Month)
            .SingleAsync(x => x.Id == id);
        return farmer;

    }


    public async Task<FarmerReportRelated> GetById(int id)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        const string query = "usp_Farmer_Get_By_Id";
        var param = new DynamicParameters();
        param.Add("@Id", id);

        using var multi = await conn.QueryMultipleAsync(query, param, commandType: CommandType.StoredProcedure);

        IEnumerable<FarmerReportRelated> farmerReport = multi.Read<FarmerReportRelated>().ToList();
        IEnumerable<FarmerMemberReportRelated> farmerMemberReport = multi.Read<FarmerMemberReportRelated>().ToList();
        IEnumerable<FarmerMemberPropertyReportRelated> farmerMemberPropertyReport = multi.Read<FarmerMemberPropertyReportRelated>().ToList();
        IEnumerable<LastSeasonProductionReportRelated> lastSeasonProductionReport = multi.Read<LastSeasonProductionReportRelated>().ToList();
        IEnumerable<ProductionPlanReportRelated> productionPlanReport = multi.Read<ProductionPlanReportRelated>().ToList();
        IEnumerable<ResearchReportRelated> researchReport = multi.Read<ResearchReportRelated>().ToList();
        IEnumerable<ResearchInternetUseReportRelated> researchInternetUseReportRelated = multi.Read<ResearchInternetUseReportRelated>().ToList();
        IEnumerable<ResearchObstacleReportRelated> researchObstacleReportRelated = multi.Read<ResearchObstacleReportRelated>().ToList();
        IEnumerable<ResearchMarkerStatusRelated> markerStatusRelated = multi.Read<ResearchMarkerStatusRelated>().ToList();


        foreach (var farmerMember in farmerMemberReport)
        {
            farmerMember.FarmerMemberProperties = farmerMemberPropertyReport.Where(x => x.FarmerMemberId == farmerMember.Id);
        }
        foreach (var research in researchReport)
        {
            var internetUses = researchInternetUseReportRelated.Where(x => x.ResearchId == research.Id);
            var productionObstacle = researchObstacleReportRelated.Where(x => x.ResearchId == research.Id && x.IsProduction);
            var saleObstacle = researchObstacleReportRelated.Where(x => x.ResearchId == research.Id && x.IsSale);
            research.InternetUses = string.Join(",", internetUses.Select(x => x.InternetUseNepaliName));
            research.ProductionObstacles = string.Join(",", productionObstacle.Select(x => x.ObstacleNepaliName));
            research.SalesObstacles = string.Join(",", saleObstacle.Select(x => x.ObstacleNepaliName));
            research.MarketStatuses = markerStatusRelated.Where(x => x.ResearchId == research.Id);
        }


        foreach (var farmerReportRelated in farmerReport)
        {
            farmerReportRelated.FarmerMembers = farmerMemberReport.Where(x => x.FarmerId == farmerReportRelated.Id);
            farmerReportRelated.LastSeasonProductions = lastSeasonProductionReport.Where(x => x.FarmerId == farmerReportRelated.Id);
            farmerReportRelated.ProductionPlans = productionPlanReport.Where(x => x.FarmerId == farmerReportRelated.Id);
            farmerReportRelated.Researches = researchReport.Where(x => x.FarmerId == farmerReportRelated.Id);
        }



        var farmerReportDto = farmerReport.FirstOrDefault();

        //Farmer farmer = JsonConvert.DeserializeObject<Farmer>(JsonConvert.SerializeObject(farmerReportDto));



        return farmerReportDto ?? new FarmerReportRelated();



    }

    public async Task<string> ChangePicture(string name, int id)
    {
        Farmer farmer = await _appDbContext.Farmers.SingleAsync(x => x.Id == id);
        string oldPhoto = farmer.PhotoName ?? string.Empty;
        farmer.PhotoName = name;
        await _appDbContext.SaveChangesAsync();
        return oldPhoto;
    }

    public async Task<IEnumerable<FarmerReportRelated>> GetFarmerForReport(int province, int districtId, int palikaId, int ward, int communityId)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        const string query = "usp_Farmer_Report";
        var param = new DynamicParameters();
        param.Add("@DistrictId", districtId);
        param.Add("@ProvinceId", province);
        param.Add("@PalikaId", palikaId);
        param.Add("@Ward", ward);
        param.Add("@CommunityId", communityId);

        using var multi = await conn.QueryMultipleAsync(query, commandType: CommandType.StoredProcedure);

        IEnumerable<FarmerReportRelated> farmerReport = multi.Read<FarmerReportRelated>().ToList();
        IEnumerable<FarmerMemberReportRelated> farmerMemberReport = multi.Read<FarmerMemberReportRelated>().ToList();
        IEnumerable<FarmerMemberPropertyReportRelated> farmerMemberPropertyReport = multi.Read<FarmerMemberPropertyReportRelated>().ToList();
        IEnumerable<LastSeasonProductionReportRelated> lastSeasonProductionReport = multi.Read<LastSeasonProductionReportRelated>().ToList();
        IEnumerable<ProductionPlanReportRelated> productionPlanReport = multi.Read<ProductionPlanReportRelated>().ToList();
        IEnumerable<ResearchReportRelated> researchReport = multi.Read<ResearchReportRelated>().ToList();
        IEnumerable<ResearchInternetUseReportRelated> researchInternetUseReportRelated = multi.Read<ResearchInternetUseReportRelated>().ToList();
        IEnumerable<ResearchObstacleReportRelated> researchObstacleReportRelated = multi.Read<ResearchObstacleReportRelated>().ToList();
        IEnumerable<ResearchMarkerStatusRelated> markerStatusRelated = multi.Read<ResearchMarkerStatusRelated>().ToList();


        //foreach (var farmerMember in farmerMemberReport)
        //{
        //    farmerMember.FarmerMemberProperties = farmerMemberPropertyReport.Where(x => x.FarmerMemberId == farmerMember.Id);
        //}
        foreach (var research in researchReport)
        {
            var internetUses = researchInternetUseReportRelated.Where(x => x.ResearchId == research.Id);
            var productionObstacle = researchObstacleReportRelated.Where(x => x.ResearchId == research.Id && x.IsProduction);
            var saleObstacle = researchObstacleReportRelated.Where(x => x.ResearchId == research.Id && x.IsSale);
            research.InternetUses = string.Join(",", internetUses.Select(x => x.InternetUseNepaliName));
            research.ProductionObstacles = string.Join(",", productionObstacle.Select(x => x.ObstacleNepaliName));
            research.SalesObstacles = string.Join(",", saleObstacle.Select(x => x.ObstacleNepaliName));
            research.MarketStatuses = markerStatusRelated.Where(x => x.ResearchId == research.Id);
        }


        foreach (var farmerReportRelated in farmerReport)
        {
            farmerReportRelated.FarmerMembers = farmerMemberReport.Where(x => x.FarmerId == farmerReportRelated.Id);
            farmerReportRelated.LastSeasonProductions = lastSeasonProductionReport.Where(x => x.FarmerId == farmerReportRelated.Id);
            farmerReportRelated.ProductionPlans = productionPlanReport.Where(x => x.FarmerId == farmerReportRelated.Id);
            farmerReportRelated.Researches = researchReport.Where(x => x.FarmerId == farmerReportRelated.Id);
        }






        return farmerReport;


    }

    public Task UpdateAsync(Farmer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Farmer> FarmerActiveStatusChanged(int farmerId, string modifiedBy, DateTime modifiedDate)
    {
        Farmer farmer = await _appDbContext.Farmers.SingleAsync(x => x.Id == farmerId);

        farmer.IsActive = !farmer.IsActive;
        farmer.ModifiedBy = modifiedBy;
        farmer.ModifiedDate = modifiedDate;
        await _appDbContext.SaveChangesAsync();
        return farmer;
    }

    public async Task<int> ResearchDataSync(Research research, int farmerId)
    {
        await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {


            if (farmerId != 0)
            {
                Research? aleradyInsertedResearch = await _appDbContext.Researches.FirstOrDefaultAsync(x => x.FarmerId == farmerId);

                if (aleradyInsertedResearch != null)
                {



                    var participantOptions = await _appDbContext.FarmerMemberAgricultureParticipants.Where(x => x.ResearchId == aleradyInsertedResearch.Id).ToListAsync();
                    if (participantOptions.Any())
                    {
                        _appDbContext.RemoveRange(participantOptions);
                        await _appDbContext.SaveChangesAsync();
                    }

                    var marketStatus = await _appDbContext.MarketStatuses.Where(x => x.ResearchId == aleradyInsertedResearch.Id).ToListAsync();

                    if (marketStatus.Any())
                    {
                        _appDbContext.RemoveRange(marketStatus);
                        await _appDbContext.SaveChangesAsync();

                    }

                    var researchInternetUse = await _appDbContext.ResearchInternetUses.Where(x => x.ResearchId == aleradyInsertedResearch.Id).ToListAsync();

                    if (researchInternetUse.Any())
                    {
                        _appDbContext.RemoveRange(researchInternetUse);
                        await _appDbContext.SaveChangesAsync();

                    }

                    var researchObstacle = await _appDbContext.ResearchObstacles.Where(x => x.ResearchId == aleradyInsertedResearch.Id).ToListAsync();
                    if (researchObstacle.Any())
                    {
                        _appDbContext.RemoveRange(researchObstacle);
                        await _appDbContext.SaveChangesAsync();
                    }

                    _appDbContext.Remove(aleradyInsertedResearch);
                    await _appDbContext.SaveChangesAsync();

                }
            }

            Research insertedResearch = (await _appDbContext.AddAsync(research)).Entity;
            await _appDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return insertedResearch.Id;

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }


    }

    public async Task<int> LastSeasonProductionSync(LastSeasonProduction lastSeasonProduction)
    {
        var lastSeasonProductions = await _appDbContext.LastSeasonProductions.Where(x =>
            x.FarmerId == lastSeasonProduction.FarmerId && x.GrainId == lastSeasonProduction.GrainId).ToListAsync();

        if (lastSeasonProductions.Any())
        {
            throw new DataValidationException("Data has already been synced");
        }
        lastSeasonProduction.Farmer = null;
        lastSeasonProduction.Grain = null;

        EntityEntry<LastSeasonProduction> insertedLastSeasonProduction = await _appDbContext.LastSeasonProductions.AddAsync(lastSeasonProduction);
        await _appDbContext.SaveChangesAsync();
        return insertedLastSeasonProduction.Entity.Id;
    }

    public async Task<int> GetFarmerMemberIdByFarmerAndMemberSyncId(int farmerId, int memberSyncId)
    {
        var data = await _appDbContext.FarmerMembers.SingleAsync(x => x.FarmerId == farmerId && x.SyncId == memberSyncId);
        return data.Id;
    }

    public async Task<bool> CheckIfLastSeasonAlreadyExists(int farmerId, int grainId)
    {
        return await _appDbContext.LastSeasonProductions.AnyAsync(x => x.FarmerId == farmerId && x.GrainId == grainId);

    }

    public async Task<IEnumerable<Farmer>> GetFarmerHavingMemberShipId()
    {
        return await _appDbContext.Farmers.AsNoTracking().Where(x => x.FarmerMembershipNumber != null).ToListAsync();
    }


    public async Task<int> ProductionPlanSync(ProductionPlan productionPlan)
    {

        var productionPlanExist = await _appDbContext.ProductionPlans.AnyAsync(x =>
            x.FarmerId == productionPlan.FarmerId && x.GrainCycleId == productionPlan.GrainCycleId &&
            x.GrainId == productionPlan.GrainId);

        if (productionPlanExist)
        {
            throw new DataValidationException("Production plan has already exist");
        }

        productionPlan.Id = 0;

        EntityEntry<ProductionPlan> insertedProductionPlan =
            await _appDbContext.ProductionPlans.AddAsync(productionPlan);
        await _appDbContext.SaveChangesAsync();

        return insertedProductionPlan.Entity.Id;

    }

    public async Task<FarmerReportRelated> GetFarmerReportRelatedById(int id)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        const string query = "usp_Farmer_Get_By_Id";
        var param = new DynamicParameters();
        param.Add("@Id", id);

        using var multi = await conn.QueryMultipleAsync(query, param, commandType: CommandType.StoredProcedure);
        IEnumerable<FarmerReportRelated> farmerReport = multi.Read<FarmerReportRelated>().ToList();
        var farmerReportDto = farmerReport.First();
        return farmerReportDto;

    }

    public async Task<IEnumerable<Farmer>> GetFarmerThisSeasonProduction()
    {
        var data = await _appDbContext
            .Farmers
            .Select(farmer => new Farmer()
            {
                Id = farmer.Id,
                FirstName = farmer.FirstName,
                MiddleName = farmer.MiddleName,
                LastName = farmer.LastName,
                ProductionPlans = farmer.ProductionPlans.Where(x => x.ActualSalesThroughCooperative == null && x.TotalSalesThroughCooperative > 0)
                    .Select(productionPlan => new ProductionPlan()
                    {
                        Id = productionPlan.Id,
                        TotalSalesThroughCooperative = productionPlan.TotalSalesThroughCooperative,
                        ActualSalesThroughCooperative = productionPlan.ActualSalesThroughCooperative,
                        FarmerId = productionPlan.FarmerId,
                        GrainCycle = !productionPlan.GrainCycle.IsActive ? new GrainCycle() : new GrainCycle()
                        {
                            Id = productionPlan.GrainCycle.Id,
                            Name = productionPlan.GrainCycle.Name,
                            NepaliName = productionPlan.GrainCycle.NepaliName,
                            FromMonthId = productionPlan.GrainCycle.FromMonthId,
                            ToMonthId = productionPlan.GrainCycle.ToMonthId,
                            YearId = productionPlan.GrainCycle.YearId,
                            Year = new Year()
                            {
                                Id = productionPlan.GrainCycle.Year.Id,
                                Name = productionPlan.GrainCycle.Year.Name
                            },
                            FromMonth = new Month()
                            {
                                Id = productionPlan.GrainCycle.FromMonth.Id,
                                NepaliName = productionPlan.GrainCycle.FromMonth.NepaliName,
                                Name = productionPlan.GrainCycle.FromMonth.Name
                            },
                            ToMonth = new Month()
                            {
                                Id = productionPlan.GrainCycle.ToMonth.Id,
                                NepaliName = productionPlan.GrainCycle.FromMonth.NepaliName,
                                Name = productionPlan.GrainCycle.ToMonth.Name,
                            },
                            Grain = new Grain()
                            {
                                Id = productionPlan.GrainCycle.Grain.Id,
                                Name = productionPlan.GrainCycle.Grain.Name,
                                NepaliName = productionPlan.GrainCycle.Grain.NepaliName,
                            }
                        }
                    })
                    .ToList()
            })
            .AsNoTracking()
            .ToListAsync();
        return data;
    }

    public async Task<Farmer> AddFarmer(Farmer farmer)
    {
            await _appDbContext.Farmers.AddAsync(farmer);
            await _appDbContext.SaveChangesAsync();
            return farmer;
    }

    public async Task<Farmer> UpdateIsNonMember(Farmer farmer)
    {
        Farmer farmerToBeUpdated = await _appDbContext.Farmers.SingleAsync(x => x.Id == farmer.Id);
        farmerToBeUpdated.IsNonMember = !farmerToBeUpdated.IsNonMember;
        farmerToBeUpdated.ModifiedBy = farmer.ModifiedBy;
        farmerToBeUpdated.ModifiedDate = farmer.ModifiedDate;

        await _appDbContext.SaveChangesAsync();
        return farmer;
    }
}

