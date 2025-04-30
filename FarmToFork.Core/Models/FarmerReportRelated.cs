using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Models
{
    public class FarmerReportRelated
    {
        public int Id { get; set; }
        public string? FarmerCode { get; set; }
        public string FullName { get; set; } = null!;
        public string? DateOfBirth { get; set; }
        public int Ward { get; set; }
        public string? MobileNumber { get; set; }
        public string? PhotoName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string? ModifiedBy { get; set; }
        public DateTime? SyncDate { get; set; }
        public string? HusbandOrFatherName { get; set; }
        public string? HusbandOrWifeName { get; set; }
        public int? Age { get; set; }
        public bool OwnSmartPhone { get; set; } = false;
        public string? GrandFatherOrFatherInLawName { get; set; }
        public decimal? HouseValuationAmount { get; set; }
        public decimal? BusinessValuationAmount { get; set; }
        public decimal? JewelryValuationAmount { get; set; }
        public decimal? OtherValuationAmount { get; set; }
        public string? NomineeName { get; set; }
        public string? NomineeRelationNepaliName { get; set; }
        public string? MartialStatusNepaliName { get; set; }
        public decimal? CattleValuationAmount { get; set; }
        public decimal? BankBalanceValuationAmount { get; set; }
        public string? ProvinceNepaliName { get; set; }
        public string? GenderNepaliName { get; set; }
        public string? EthnicityNepaliName { get; set; }
        public string? DistrictNepaliName { get; set; }
        public string? PalikaNepaliName { get; set; }
        public string? CommunityNepaliName { get; set; }
        public string? OccupationNepaliName { get; set; }
        public string? EducationLevelNepaliName { get; set; }
        public string? RelationNepaliName { get; set; }
        public string? CitizenshipNumber { get; set; }
        public bool IsActive { get; set; }
        public int? FarmerMembershipNumber { get; set; }
        public string? Identifier { get; set; }
        public string DistrictName { get; set; } = null!;
        public string ProvinceName { get; set; } = null!;
        public string PalikaName { get; set; } = null!;
        public string CommunityName { get; set; } = null!;
        public string? EthnicityName { get; set; } 
        public string? GenderName { get; set; } 
        public string? MaritalStatusName { get; set; }
        public string? OccupationName { get; set; } 
        public string? EducationLevelName { get; set; } 
        public string? NomineeRelationName { get; set; }

        public string QRCode { get; set; } = string.Empty;
        public IEnumerable<FarmerMemberReportRelated> FarmerMembers { get; set; } = new List<FarmerMemberReportRelated>();
        public IEnumerable<LastSeasonProductionReportRelated> LastSeasonProductions { get; set; } = new List<LastSeasonProductionReportRelated>();
        public IEnumerable<ProductionPlanReportRelated> ProductionPlans { get; set; } = new List<ProductionPlanReportRelated>();
        public IEnumerable<ResearchReportRelated> Researches { get; set; } = new List<ResearchReportRelated>();

    }

    public class FarmerMemberReportRelated
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public string? Name { get; set; }
        public string? RelationNepaliName { get; set; }
        public string? OccupationNepaliName { get; set; }
        public string? EducationLevelNepaliName { get; set; }
        public string? GenderNepaliName { get; set; }
        public int Age { get; set; }
        public bool OwnSmartPhone { get; set; }
        public string? Remarks { get; set; }
        public string? MobileNumber { get; set; }
        public int? Ward { get; set; }
        public string? LocationNepaliName { get; set; }
        public string? IncomeSourceNepaliName { get; set; }
        public decimal? IncomeAmount { get; set; }
        public decimal? ExpenseAmount { get; set; }
        public decimal? SavingAmount { get; set; }
        public DateTime? SyncDate { get; set; }
        public string RelationName { get; set; } = null!;
        public string OccupationName { get; set; } = null!;
        public string EducationLevelName { get; set; } = null!;
        public string GenderName { get; set; } = null!;
        public IEnumerable<FarmerMemberPropertyReportRelated> FarmerMemberProperties { get; set; } = Enumerable.Empty<FarmerMemberPropertyReportRelated>();

    }

    public class FarmerMemberPropertyReportRelated
    {
        public int Id { get; set; }
        public int FarmerMemberId { get; set; }
        public string DistrictNepaliName { get; set; } = null!;
        public string PalikaNepaliName { get; set; } = null!;
        public string ProvinceNepaliName { get; set; } = null!;
        public string DistrictName { get; set; } = null!;
        public string PalikaName { get; set; } = null!;
        public string ProvinceName { get; set; } = null!;
        public int? Ward { get; set; }
        public string? KittaNumber { get; set; }
        public decimal? LandArea { get; set; }
        public decimal? LandValuation { get; set; }
        public string? Tole { get; set; }
        public DateTime? SyncDate { get; set; }
        

    }
    public class LastSeasonProductionReportRelated
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public string? GrainNepaliName { get; set; }
        public decimal? ProductionLandArea { get; set; }
        public decimal? TotalProduction { get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? TotalSalesAmount { get; set; }

    }
    public class ProductionPlanReportRelated
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public string? GrainNepaliName { get; set; }
        public string? GrainCycleNepaliName { get; set; }
        public decimal? LandArea { get; set; }
        public decimal? TotalProduction { get; set; }
        public decimal? HomeUse { get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? TotalSalesThroughCooperative { get; set; }
        public decimal? SeedOnly { get; set; }
    }

    public class ResearchReportRelated {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public int? EconomicallyActivePeople { get; set; }
        public bool HasInternetConnection { get; set; }
        public string? InternetTypeNepaliName { get; set; }
        public string? InternetTypeName { get; set; }
        public string? HarvestingAndStoringAfterBeanProductionNepali { get; set; }
        public string? LandOwnershipForBeanProductionNepali { get; set; }
        public string? MakingLandReadyForBeanProductionNepali { get; set; }
        public string? PlantSeedForBeanProductionNepali { get; set; }
        public string? SellingAndMarketingAfterBeanProductionNepali { get; set; }
        public string? TakingCareForBeanProductionNepali { get; set; }
        public decimal? TotalLandArea { get; set; }

        public string? InternetUses { get; set; }
        public string? ProductionObstacles { get; set; }
        public string? SalesObstacles { get; set; }
        public decimal? LandAreaForBeanProduction { get; set; }
        public IEnumerable<ResearchFarmerMemberParticipantReportRelated> FarmerMemberAgricultureParticipants { get; set; } = new List<ResearchFarmerMemberParticipantReportRelated>();
        public IEnumerable<ResearchMarkerStatusRelated> MarketStatuses { get; set; } = new List<ResearchMarkerStatusRelated>();
        public IEnumerable<ResearchObstacleReportRelated> ResearchObstacles { get; set; } = new List<ResearchObstacleReportRelated>();
        public IEnumerable<ResearchInternetUseReportRelated> ResearchInternetUses { get; set; } = new List<ResearchInternetUseReportRelated>();
    }

    public class ResearchInternetUseReportRelated
    {
        public int Id { get; set; }
        public int ResearchId { get; set; }
        public string? InternetUseNepaliName { get; set; }
    }

    public class ResearchObstacleReportRelated
    {
        public int Id { get; set; }
        public int ResearchId { get; set; }
        public string? ObstacleNepaliName { get; set; }
        public bool IsProduction { get; set; }
        public bool IsSale { get; set; }
    }
    
    public class ResearchFarmerMemberParticipantReportRelated
    {
        public int Id { get; set; }
        public int ResearchId { get; set; }
        public int FarmerMemberId { get; set; }
        public string? ParticipantOptionNepaliName { get; set; }
        public int? SyncId { get; set; }
        public DateTime? SyncDate { get; set; }
    }

    public class ResearchMarkerStatusRelated
    {
        public int Id { get; set; }
        public int FarmerId { get; set; }
        public int ResearchId { get; set; }
        public string? GrainNepaliName { get; set; }
        public string? MonthNepaliName { get; set; }
        public int? Week { get; set; }
        public decimal? SaleRate { get; set; }
        public bool IsFromHome { get; set; }
        public int? SyncId { get; set; }
    }

}
