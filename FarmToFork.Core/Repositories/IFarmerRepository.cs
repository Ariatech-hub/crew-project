namespace FarmToFork.Core.Repositories;
public interface IFarmerRepository : IRepository<Farmer>
{
    public Task<int> FarmerSync(Farmer farmer, DataTable farmerMemberTable, DataTable farmerMemberPropertyTable, DataTable farmerMemberTransactionTable);

    public Task<IEnumerable<FarmerReportRelated>> GetFarmerForReport(int province, int districtId, int palikaId, int ward, int communityId);

    public Task<int> ProductionPlanSync(ProductionPlan productionPlan);

    public Task<Farmer> FarmerActiveStatusChanged(int farmerId, string modifiedBy, DateTime modifiedDate);

    public Task<int> ResearchDataSync(Research research, int farmerId);

    public Task<int> LastSeasonProductionSync(LastSeasonProduction lastSeasonProduction);

    public Task<int> GetFarmerMemberIdByFarmerAndMemberSyncId(int farmerId, int memberSyncId);

    public Task<bool> CheckIfLastSeasonAlreadyExists(int farmerId, int grainId);


    public  Task<IEnumerable<Farmer>> GetFarmerHavingMemberShipId();

    public Task<FarmerReportRelated> GetById(int id);
     
    public Task<string> ChangePicture(string name , int id);


    public Task<FarmerReportRelated> GetFarmerReportRelatedById(int id);

    public Task<IEnumerable<Farmer>> GetFarmerThisSeasonProduction();

    Task<Farmer> AddFarmer(Farmer farmer);
    Task<Farmer> UpdateIsNonMember(Farmer farmer);

}

