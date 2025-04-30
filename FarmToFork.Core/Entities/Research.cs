using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Research
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

    public DateTime CreatedDate { get; set; }

    public DateTime? SyncDate { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual ICollection<FarmerMemberAgricultureParticipant> FarmerMemberAgricultureParticipants { get; set; } = new List<FarmerMemberAgricultureParticipant>();

    public virtual LabourDivision? HarvestingAndStoringAfterBeanProductionLabourDivision { get; set; }

    public virtual LabourDivision? LandOwnershipForBeanProductionLabourDivision { get; set; }

    public virtual LabourDivision? MakingLandReadyForBeanProductionLabourDivision { get; set; }

    public virtual ICollection<MarketStatus> MarketStatuses { get; set; } = new List<MarketStatus>();

    public virtual LabourDivision? PlantSeedForBeanProductionLabourDivision { get; set; }

    public virtual ICollection<ResearchInternetUse> ResearchInternetUses { get; set; } = new List<ResearchInternetUse>();

    public virtual ICollection<ResearchObstacle> ResearchObstacles { get; set; } = new List<ResearchObstacle>();

    public virtual LabourDivision? SellingAndMarketingAfterBeanProductionLabourDivision { get; set; }

    public virtual LabourDivision? TakingCareForBeanProductionLabourDivision { get; set; }

    public virtual InternetType? TypeOfInternet { get; set; }
}
