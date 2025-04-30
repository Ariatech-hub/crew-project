using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Core.Entities;
using FluentValidation;

namespace FarmToFork.Business.DTOs;

public class FarmerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public int? GenderId { get; set; }
    public int? EthnicityId { get; set; }
    public int? MaritalStatusId { get; set; }
    public string? DateOfBirth { get; set; }
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int PalikaId { get; set; }
    public int Ward { get; set; }
    public string? Tole { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhotoName { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string CreatedBy { get; set; } = null!;
    public long? CreatedDate { get; set; }
    public string? HusbandOrWifeName { get; set; }
    public string? ModifiedBy { get; set; }
    public string? FullName { get; set; }
    public int? CommunityId { get; set; }
    public string? CitizenshipNumber { get; set; }
    public string? HusbandOrFatherName { get; set; }
    public int? OccupationId { get; set; }
    public int? EducationLevelId { get; set; }
    public int? Age { get; set; }
    public bool? OwnSmartPhone { get; set; }
    public string? GrandFatherOrFatherInLawName { get; set; }
    public decimal? HouseValuationAmount { get; set; }
    public decimal? OtherValuationAmount { get; set; }
    public decimal? BusinessValuationAmount { get; set; }
    public decimal? JewelryValuationAmount { get; set; }
    public string? NomineeName { get; set; }
    public int? NomineeRelationshipId { get; set; }
    public string Identifier { get; set; } = null!;
    public int? SyncId { get; set; }
    public int? FarmerMembershipNumber { get; set; }
    public decimal? CattleValuationAmount { get; set; }
    public decimal? BankBalanceValuationAmount { get; set; }
    public bool? IsNonMember { get; set; }




}

public class FarmerCooperativeInitialData
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string? Code { get; set; }
}

public class FarmerMemberShipIdDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int PalikaId { get; set; }
    public int Ward { get; set; }
    public int? CommunityId { get; set; }
    public string? MobileNumber { get; set; }
    public string Code { get; set; } = string.Empty;

    public int? FarmerMembershipNumber { get; set; }


}


public class FarmerMemberDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int RelationId { get; set; }
    public string? RelationName { get; set; }
    public string? OccupationName { get; set; }
    public string? EducationLevelName { get; set; }
    public string? GenderName { get; set; }
    public int? OccupationId { get; set; }
    public int? LocationId { get; set; }
    public int? EducationLevelId { get; set; }
    public int? GenderId { get; set; }
    public int? Age { get; set; }
    public bool OwnSmartPhone { get; set; }
    public string? Remarks { get; set; }
    public long? CreatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? Identifier { get; set; }

    public string? MobileNumber { get; set; }
}

public partial class FarmerMemberPropertyDto
{
    public int Id { get; set; }
    public int FarmerMemberId { get; set; }
    public int? DistrictId { get; set; }
    public string DistrictName { get; set; } = null!;
    public string PalikaName { get; set; } = null!;
    public string ProvinceName { get; set; } = null!;
    public int? PalikaId { get; set; }
    public int? ProvinceId { get; set; }
    public int? Ward { get; set; }
    public string? KittaNumber { get; set; }
    public decimal? LandArea { get; set; }
    public decimal? LandValuation { get; set; }
    public long? CreatedDate { get; set; }
    public DateTime? SyncDate { get; set; }
    public string? MemberIdentifier { get; set; }
    public string? Tole { get; set; }
}

public class FarmerMemberTransactionDto
{
    public int Id { get; set; }
    public int FarmerMemberId { get; set; }
    public string? IncomeSource { get; set; }
    public decimal? IncomeAmount { get; set; }
    public string? ExpenseSource { get; set; }
    public decimal? ExpenseAmount { get; set; }
    public long? CreatedDate { get; set; }
    public string? MemberIdentifier { get; set; }
}

public class FarmerDtoVm
{
    public FarmerDto Farmer { get; set; } = new FarmerDto();

    public IEnumerable<FarmerMemberTransactionDto> FarmerMemberTransaction { get; set; } = new List<FarmerMemberTransactionDto>();

    public IEnumerable<FarmerMemberPropertyDto> FarmerMemberProperty { get; set; } = new List<FarmerMemberPropertyDto>();

    public IEnumerable<FarmerMemberDto> FarmerMember { get; set; } = new List<FarmerMemberDto>();
    public Enumerator? Enumerator { get; set; }



}

public class EnumeratorDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string? Location { get; set; }
    public string? DeviceId { get; set; }
    public int? AspnetuserId { get; set; }
}

public class FarmerReportDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public int GenderId { get; set; }
    public int? EthnicityId { get; set; }
    public int? MaritalStatusId { get; set; }
    public string? DateOfBirth { get; set; }
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int PalikaId { get; set; }
    public int Ward { get; set; }
    public string? Tole { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhotoName { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string FullName { get; set; } = null!;
    public int? CommunityId { get; set; }
    public string? HusbandOrFatherName { get; set; }
    public int? OccupationId { get; set; }
    public int? EducationLevelId { get; set; }
    public int? Age { get; set; }
    public bool OwnSmartPhone { get; set; } = false;
    public string? GrandFatherOrFatherInLawName { get; set; }
    public decimal? HouseValuationAmount { get; set; }
    public decimal? BusinessValuationAmount { get; set; }
    public decimal? JewelryValuationAmount { get; set; }

    public decimal? OtherValuationAmount { get; set; }
    public string? NomineeName { get; set; }
    public string? NomineeRelationshipName { get; set; }
    public int? NomineeRelationshipId { get; set; }

    public string? MaritalStatusName { get; set; }

    public DateTime SyncDate { get; set; }
    public decimal? CattleValuationAmount { get; set; }
    public decimal? BankBalanceValuationAmount { get; set; }
    public int? SyncId { get; set; }
    public string? ProvinceName { get; set; }
    public string? GenderName { get; set; }
    public string? EthnicityName { get; set; }
    public string? DistrictName { get; set; }
    public string? PalikaName { get; set; }
    public string? CommunityName { get; set; }
    public string? OccupationName { get; set; }
    public string? EducationLevelName { get; set; }
    public string? RelationName { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<FarmerMemberReportDto> FarmerMembers { get; set; } = new List<FarmerMemberReportDto>();

    public IEnumerable<LastSeasonProductionDto> LastSeasonProductions { get; set; } = new List<LastSeasonProductionDto>();
    public IEnumerable<ProductionPlanViewDto> ProductionPlans { get; set; } = new List<ProductionPlanViewDto>();
    public IEnumerable<ResearchViewDto> Researches { get; set; } = new List<ResearchViewDto>();
}

public class FarmerMemberReportDto
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public string Name { get; set; } = null!;
    public int? RelationId { get; set; }
    public int? OccupationId { get; set; }
    public int? EducationLevelId { get; set; }
    public int? GenderId { get; set; }
    public int? Age { get; set; }
    public bool OwnSmartPhone { get; set; }
    public string? Remarks { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? Identifier { get; set; }
    public string? MobileNumber { get; set; }
    public DateTime SyncDate { get; set; }
    public int? SyncId { get; set; }
    public string? RelationName { get; set; }
    public string? OccupationName { get; set; }
    public string? EducationLevelName { get; set; }
    public string? GenderName { get; set; }

    public IEnumerable<FarmerMemberPropertyReportDto> FarmerMemberProperties { get; set; } = new List<FarmerMemberPropertyReportDto>();
    public IEnumerable<FarmerMemberTransactionReportDto> FarmerMemberTransactions { get; set; } = new List<FarmerMemberTransactionReportDto>();
}
public class FarmerMemberPropertyReportDto
{
    public int Id { get; set; }
    public int FarmerMemberId { get; set; }
    public int? DistrictId { get; set; }
    public int? PalikaId { get; set; }
    public int? ProvinceId { get; set; }
    public int? Ward { get; set; }
    public string? KittaNumber { get; set; }
    public decimal? LandArea { get; set; }
    public decimal? LandValuation { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? SyncDate { get; set; }
    public string? MemberIdentifier { get; set; }
    public string? Tole { get; set; }
    public int? SyncId { get; set; }

    public string DistrictName { get; set; } = null!;
    public string PalikaName { get; set; } = null!;
    public string ProvinceName { get; set; } = null!;
}

public class FarmerMemberTransactionReportDto
{
    public int Id { get; set; }
    public int? FarmerMemberId { get; set; }
    public string? IncomeSource { get; set; }
    public decimal? IncomeAmount { get; set; }
    public string? ExpenseSource { get; set; }
    public decimal? ExpenseAmount { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? SyncDate { get; set; }
    public string? MemberIdentifier { get; set; }
    public int? SyncId { get; set; }
}


public class FarmerProductionSyncDto
{
    public FarmerDto Farmer { get; set; } = new FarmerDto();
    public ProductionPlanDto ProductionPlanData { get; set; } = new ProductionPlanDto();
    public EnumeratorDto Enumerator { get; set; } = new EnumeratorDto();

}

public class LastSeasonSyncDto
{
    public FarmerDto Farmer { get; set; } = new FarmerDto();

    public LastSeasonProductionDto LastSeasonProductionData { get; set; } = new LastSeasonProductionDto();

    public EnumeratorDto Enumerator { get; set; } = new EnumeratorDto();
}




public class ProductionPlanDto
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public int GrainId { get; set; }
    public int GrainCycleId { get; set; }
    public decimal? LandArea { get; set; }
    public decimal? TotalProduction { get; set; }
    public decimal? HomeUse { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? TotalSalesThroughCooperative { get; set; }
    public decimal? SeedOnly { get; set; }
    public decimal? ActualSalesThroughCooperative { get; set; }
    public string? Remarks { get; set; }
    public decimal? UnitPrice { get; set; }
}




public class FarmerNonMemberProductionPlanDto
{
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int PalikaId { get; set; }
    public int Ward { get; set; }
    public IEnumerable<ProductionPlanDto> ProductionPlan { get; set; } = Enumerable.Empty<ProductionPlanDto>();
}





public class FarmerMemberAgricultureParticipantDto
{
    public int Id { get; set; }
    public int ProductionPlanId { get; set; }
    public int FarmerMemberId { get; set; }
    public string FarmerMemberName { get; set; } = string.Empty;
    public string MemberIdentifier { get; set; } = null!;
    public int ParticipantOptionId { get; set; }
    public string? ParticipantOptionName { get; set; }





}

public class LastSeasonProductionDto
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public int GrainId { get; set; }
    public string? GrainName { get; set; }
    public decimal? ProductionLandArea { get; set; }
    public decimal? TotalProduction { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? TotalSalesAmount { get; set; }
    public string? GrainNepaliName { get; set; }


}

public class MarketStatusDto
{
    public int Id { get; set; }
    public int ResearchId { get; set; }
    public int GrainId { get; set; }
    public decimal? TotalSalesNumber { get; set; }
    public int? MonthId { get; set; }
    public string? MonthName { get; set; }
    public string? GrainName { get; set; }
    public decimal? SaleRate { get; set; }
    public bool IsFromHome { get; set; }
    public int? SyncId { get; set; }
    public int? Week { get; set; }
    public DateTime? SyncDate { get; set; }
    public DateTime? CreatedDate { get; set; }


}

public class ProductionPlanGrainQuantityDto
{
    public int Id { get; set; }
    public int? ProductionPlanId { get; set; }
    public int? GrainTypeId { get; set; }
    public decimal? ProductionLandArea { get; set; }
    public decimal? TotalProduction { get; set; }
    public decimal? HomeUse { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? TotalSalesThroughCooperative { get; set; }
    public decimal? SeedOnly { get; set; }

}

public class FarmerTableDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public int GenderId { get; set; }
    public string? GenderName { get; set; }
    public string? DistrictName { get; set; }
    public string? PalikaName { get; set; }
    public string? CommunityName { get; set; }
    public int Ward { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string? Tole { get; set; }
    public bool IsActive { get; set; }
    public int ProvinceId { get; set; }
    public int DistrictId { get; set; }
    public int PalikaId { get; set; }
    public string? ProvinceName { get; set; }
    public bool IsSelected { get; set; }
    public string Code { get; set; } = string.Empty;
    public bool? IsNonMember { get; set; }
}

public class FarmerActiveStatusChangeDto
{
    public int Id { get; set; }
}

public class FarmerActiveStatusChangeDtoValidation : AbstractValidator<FarmerActiveStatusChangeDto>
{
    public FarmerActiveStatusChangeDtoValidation()
    {
        _ = RuleFor(v => v.Id).NotNull().NotEqual(0).WithMessage("Invalid Farmer Id ");
    }
}


public class FarmerViewDto
{
    public int Id { get; set; }
    public string? GenderName { get; set; }
    public string? EthnicityName { get; set; }
    public string? MaritalStatusName { get; set; }
    public string? DateOfBirth { get; set; }
    public string ProvinceName { get; set; } = null!;
    public string DistrictName { get; set; } = null!;
    public string PalikaName { get; set; } = null!;
    public int Ward { get; set; }
    public string? Tole { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhotoName { get; set; }
    public string? PhotoPath { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string FullName { get; set; } = null!;
    public string? CommunityName { get; set; }
    public string? HusbandOrFatherName { get; set; }
    public string? OccupationName { get; set; }
    public string? EducationLevelName { get; set; }
    public int? Age { get; set; }
    public bool? OwnSmartPhone { get; set; }
    public string? GrandFatherOrFatherInLawName { get; set; }
    public decimal? HouseValuationAmount { get; set; }
    public decimal? BusinessValuationAmount { get; set; }
    public decimal? JewelryValuationAmount { get; set; }
    public string? NomineeName { get; set; }
    public string? NomineeRelationName { get; set; }
    public decimal? CattleValuationAmount { get; set; }
    public decimal? BankBalanceValuationAmount { get; set; }
    public decimal? OtherValuationAmount { get; set; }
    public bool IsActive { get; set; }
    public string? CitizenshipNumber { get; set; }

    public IEnumerable<FarmerMemberViewDto> FarmerMembers { get; set; } = new List<FarmerMemberViewDto>();

    public IEnumerable<LastSeasonProductionDto> LastSeasonProductions { get; set; } = new List<LastSeasonProductionDto>();
    public IEnumerable<ProductionPlanViewDto> ProductionPlans { get; set; } = new List<ProductionPlanViewDto>();
    public IEnumerable<ResearchViewDto> Researches { get; set; } = new List<ResearchViewDto>();
}

public class ProductionPlanViewDto
{
    public int Id { get; set; }
    public int GrainId { get; set; }
    public string? GrainName { get; set; }
    public int GrainCycleId { get; set; }
    public string? GrainCycleName { get; set; }
    public int GrainCycleYearId { get; set; }
    public decimal? LandArea { get; set; }
    public decimal? TotalProduction { get; set; }
    public decimal? HomeUse { get; set; }
    public decimal? TotalSales { get; set; }
    public decimal? TotalSalesThroughCooperative { get; set; }
    public decimal? SeedOnly { get; set; }
    public string FarmerFullName { get; set; } = string.Empty;
    public string FarmerCode { get; set; } = string.Empty;
    public string FarmerCommunityName { get; set; } = string.Empty;
    public int FarmerCommunityId { get; set; }
    public string GrainCycleYearName { get; set; } = string.Empty;
    public string GrainCycleFromMonthName { get; set; } = string.Empty;
    public string? GrainCycleNepaliName { get; set; }
    public string? GrainNepaliName { get; set; }

}

public class ProductionPlanSearchDto
{
    public List<int> YearIds { get; set; } = new List<int>();
    public List<int> GrainIds { get; set; } = new List<int>();
    public List<int> CommunityIds { get; set; } = new List<int>();
}

public class ResearchViewDto
{
    public int? EconomicallyActivePeople { get; set; }
    public bool HasInternetConnection { get; set; }
    public string? InternetTypeName { get; set; }
    public string? MakingLandReadyForBeanProductionLabourDivisionName { get; set; }
    public string? PlantSeedForBeanProductionLabourDivisionName { get; set; }
    public string? TakingCareForBeanProductionLabourDivisionName { get; set; }
    public string? HarvestingAndStoringAfterBeanProductionLabourDivisionName { get; set; }
    public string? SellingAndMarketingAfterBeanProductionLabourDivisionName { get; set; }
    public string? LandOwnershipForBeanProductionLabourDivisionName { get; set; }
    public decimal? TotalLandArea { get; set; }
    public decimal? LandAreaForBeanProduction { get; set; }

    public IEnumerable<FarmerMemberAgricultureParticipantDto> FarmerMemberAgricultureParticipants { get; set; } = new List<FarmerMemberAgricultureParticipantDto>();
    public IEnumerable<MarketStatusDto> MarketStatuses { get; set; } = new List<MarketStatusDto>();
    public IEnumerable<ResearchObstacleViewDto> ResearchObstacles { get; set; } = new List<ResearchObstacleViewDto>();
    public IEnumerable<ResearchInternetUseViewDto> ResearchInternetUses { get; set; } = new List<ResearchInternetUseViewDto>();
}


public class ResearchInternetUseViewDto
{
    public string? UseOfInternetName { get; set; }
}

public class ResearchObstacleViewDto
{
    public string? ObstacleName { get; set; }
    public bool IsProduction { get; set; }
    public bool IsSale { get; set; }

}

public class FarmerMemberViewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string RelationName { get; set; } = null!;
    public string OccupationName { get; set; } = null!;
    public string EducationLevelName { get; set; } = null!;
    public string GenderName { get; set; } = null!;
    public bool OwnSmartPhone { get; set; }
    public string? Remarks { get; set; }
    public int? Age { get; set; }
    public string? LocationName { get; set; }
    public virtual Location? Location { get; set; }

    public IEnumerable<FarmerMemberPropertyViewDto> FarmerMemberProperties { get; set; } = new List<FarmerMemberPropertyViewDto>();
    public IEnumerable<FarmerMemberTransactionViewDto> FarmerMemberTransactions { get; set; } = new List<FarmerMemberTransactionViewDto>();

}

public class FarmerMemberPropertyViewDto
{
    public int Id { get; set; }
    public string ProvinceName { get; set; } = null!;
    public string DistrictName { get; set; } = null!;
    public string PalikaName { get; set; } = null!;
    public int Ward { get; set; }
    public string? KittaNumber { get; set; }
    public decimal? LandArea { get; set; }
    public decimal? LandValuation { get; set; }
}

public class FarmerMemberTransactionViewDto
{
    public int Id { get; set; }
    public string? IncomeSource { get; set; }
    public decimal? IncomeAmount { get; set; }
    public string? ExpenseSource { get; set; }
    public decimal? ExpenseAmount { get; set; }
}






public class ResearchDataSyncDto
{
    public FarmerDto Farmer { get; set; } = new FarmerDto();
    public ResearchDto ResearchData { get; set; } = new ResearchDto();

    public IEnumerable<FarmerMemberParticipantDto> FarmerMemberAgricultureParticipantData { get; set; } = new List<FarmerMemberParticipantDto>();

    public IEnumerable<MarketStatusDto> MarketStatusData { get; set; } = new List<MarketStatusDto>();
    public EnumeratorDto Enumerator { get; set; } = new EnumeratorDto();
}


public class ResearchDto
{
    public int Id { get; set; }
    public int FarmerId { get; set; }
    public int? EconomicallyActivePeople { get; set; }
    public bool? HasInternetConnection { get; set; }
    public int? TypeOfInternetId { get; set; }
    public int? MakingLandReadyForBeanProductionLabourDivisionId { get; set; }
    public int? PlantSeedForBeanProductionLabourDivisionId { get; set; }
    public int? TakingCareForBeanProductionLabourDivisionId { get; set; }
    public int? HarvestingAndStoringAfterBeanProductionLabourDivisionId { get; set; }
    public int? SellingAndMarketingAfterBeanProductionLabourDivisionId { get; set; }
    public int? LandOwnershipForBeanProductionLabourDivisionId { get; set; }
    public decimal? TotalLandArea { get; set; }
    public decimal? LandAreaForBeanProduction { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UseOfInternetIds { get; set; }
    public string? ProductionObstacleIds { get; set; }
    public string? SaleObstacleIds { get; set; }
    public long? CreatedDate { get; set; }


    public int? SyncId { get; set; }


}

public class FarmerMemberParticipantDto
{
    public int Id { get; set; }
    public int ParticipantOptionId { get; set; }
    public int? SyncId { get; set; }
    public int FarmerMemberId { get; set; }
    public int ResearchId { get; set; }
    public DateTime? SyncDate { get; set; }
    public DateTime? CreatedDate { get; set; }
}

public class FarmerCardDto
{
    public int Id { get; set; }
    public string? FarmerIds { get; set; }
    public bool IsCardFlipped { get; set; }
}

public class FarmerCardView
{
    public FarmerCardView()
    {
        Farmers = new List<FarmerReportRelated>();
    }
    public string? ImageName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCardFlipped { get; set; }
    public IEnumerable<FarmerReportRelated> Farmers { get; set; }
}


public class FarmerLastProductionPlanDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public IEnumerable<ProductionPlanActiveGrainCycleDto> ProductionPlans { get; set; } = Enumerable.Empty<ProductionPlanActiveGrainCycleDto>();

}


public class ProductionPlanActiveGrainCycleDto
{
    public int Id { get; set; }
    public decimal? TotalSalesThroughCooperative { get; set; }
    public string GrainCycleName { get; set; } = string.Empty;
    public string GrainCycleNepaliName { get; set; } = string.Empty;
    public string GrainCycleYearName { get; set; } = string.Empty;
    public string GrainCycleFromMonthName { get; set; } = string.Empty;
    public string GrainCycleFromMonthNepaliName { get; set; } = string.Empty;
    public string GrainCycleToMonthNepaliName { get; set; } = string.Empty;
    public string GrainCycleToMonthName { get; set; } = string.Empty;
    public int GrainCycleGrainId { get; set; }
    public string GrainCycleGrainName { get; set; } = string.Empty;
    public string GrainCycleGrainNepaliName { get; set; } = string.Empty;
    public int GrainCycleYearId { get; set; }
}

public class FarmerSoldProductionPlanDto
{
    public int FarmerId { get; set; }
    public int Id { get; set; } // production plan Id 
    public decimal ActualSalesThroughCooperative { get; set; }
    public decimal UnitPrice { get; set; }
    public int GrainCycleId { get; set; }
}








