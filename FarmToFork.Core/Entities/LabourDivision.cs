using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class LabourDivision
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Code { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public int OrderId { get; set; }

    public virtual ICollection<Research> ResearchHarvestingAndStoringAfterBeanProductionLabourDivisions { get; set; } = new List<Research>();

    public virtual ICollection<Research> ResearchLandOwnershipForBeanProductionLabourDivisions { get; set; } = new List<Research>();

    public virtual ICollection<Research> ResearchMakingLandReadyForBeanProductionLabourDivisions { get; set; } = new List<Research>();

    public virtual ICollection<Research> ResearchPlantSeedForBeanProductionLabourDivisions { get; set; } = new List<Research>();

    public virtual ICollection<Research> ResearchSellingAndMarketingAfterBeanProductionLabourDivisions { get; set; } = new List<Research>();

    public virtual ICollection<Research> ResearchTakingCareForBeanProductionLabourDivisions { get; set; } = new List<Research>();
}
